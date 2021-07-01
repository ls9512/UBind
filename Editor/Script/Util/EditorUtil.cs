#if UNITY_EDITOR
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Aya.DataBinding
{
    public static class EditorUtil
    {
        #region Assembly & Type

        public static SearchableDropdownItem CreateAssemblyMenu()
        {
            var root = new SearchableDropdownItem("Assembly");
            root.AddChild(new SearchableDropdownItem(EditorStyle.NoneStr, null));
            root.AddSeparator();

            var assemblies = TypeCaches.Assemblies;
            foreach (var assembly in assemblies)
            {
                var assemblyName = assembly.GetName().Name;
                var child = new SearchableDropdownItem(assemblyName, assemblyName);
                root.AddChild(child);
            }

            return root;
        }

        public static void AssemblyMenu(string propertyName, SerializedProperty property, Action<Assembly> onClick = null)
        {
            var root = CreateAssemblyMenu();
            bool CheckNullFunc() => TypeCaches.GetAssemblyByName(property.stringValue) == null;
            void ResetFunc() => property.stringValue = null;
            string CurrentDisplayNameGetter()
            {
                var assembly = TypeCaches.GetAssemblyByName(property.stringValue);
                return assembly == null ? EditorStyle.NoneStr : assembly.GetName().Name;
            }

            void OnClick(SearchableDropdownItem item)
            {
                var assemblyName = item.Value == null ? "" : item.Value.ToString();
                var assembly = TypeCaches.GetAssemblyByName(assemblyName);
                property.stringValue = assembly != null ? assembly.GetName().Name : "";
                property.serializedObject.ApplyModifiedProperties();
                onClick?.Invoke(assembly);
            }

            SearchableDropdownList(propertyName, root, CheckNullFunc, ResetFunc, CurrentDisplayNameGetter, OnClick);
        }

        public static SearchableDropdownItem CreateTypeMenu(string assemblyName)
        {
            var root = new SearchableDropdownItem(string.IsNullOrEmpty(assemblyName) ? "No Assembly" : assemblyName + " - Type");
            root.AddChild(new SearchableDropdownItem(EditorStyle.NoneStr, null));
            root.AddSeparator();

            var assembly = TypeCaches.GetAssemblyByName(assemblyName);
            if (assembly != null)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.IsAbstract) continue;
                    if (type.IsInterface) continue;
                    if (type.IsGenericType) continue;
                    if (type.IsEnum) continue;

                    var child = new SearchableDropdownItem(type.Name, type.FullName);
                    root.AddChild(child);
                }
            }

            return root;
        }

        public static void TypeMenu(string propertyName, SerializedProperty property, string assemblyName, Action<Type> onClick = null)
        {
            var root = CreateTypeMenu(assemblyName);
            bool CheckNullFunc() => TypeCaches.GetTypeByName(assemblyName, property.stringValue) == null;
            void ResetFunc() => property.stringValue = null;
            string CurrentDisplayNameGetter()
            {
                var type = TypeCaches.GetTypeByName(assemblyName, property.stringValue);
                return type == null ? EditorStyle.NoneStr : type.Name;
            }

            void OnClick(SearchableDropdownItem item)
            {
                var type = TypeCaches.GetTypeByName(assemblyName, item.Value == null ? "" : item.Value.ToString());
                property.stringValue = type != null ? type.FullName : "";
                property.serializedObject.ApplyModifiedProperties();
                onClick?.Invoke(type);
            }

            SearchableDropdownList(propertyName, root, CheckNullFunc, ResetFunc, CurrentDisplayNameGetter, OnClick);
        }

        #endregion

        #region Components Tree Menu

        public static SearchableDropdownItem CreateComponentsTreeMenu<TComponent>(Transform parent) where TComponent : Component
        {
            var root = new SearchableDropdownItem(typeof(TComponent).Name);
            root.AddChild(new SearchableDropdownItem(EditorStyle.NoneStr, null));
            root.AddSeparator();
            CreateComponentsTreeMenuRecursion<TComponent>(root, parent, "");
            return root;
        }

        private static void CreateComponentsTreeMenuRecursion<TComponent>(SearchableDropdownItem root, Transform parent, string path) where TComponent : Component
        {
            var components = parent.GetComponents<TComponent>();
            foreach (var component in components)
            {
                var componentName = component.GetType().Name;
                var child = new SearchableDropdownItem(path + componentName, component)
                {
                    icon = EditorGUIUtility.ObjectContent(component, component.GetType()).image as Texture2D
                };

                root.AddChild(child);
            }

            if (parent.childCount <= 0) return;
            root.AddSeparator();
            for (var i = 0; i < parent.childCount; i++)
            {
                var childTrans = parent.GetChild(i);
                CreateComponentsTreeMenuRecursion<TComponent>(root, childTrans, path + childTrans.name + " \\ ");
            }
        }

        public static void ComponentTreeMenu<TComponent>(string propertyName,  SerializedProperty property, Transform parent, Action<TComponent> onClick = null) where TComponent : Component
        {
            var menu = CreateComponentsTreeMenu<TComponent>(parent);
            void OnClick(SearchableDropdownItem item)
            {
                TComponent target = null;
                if (item.Value == null)
                {
                    property.objectReferenceValue = null;
                }
                else
                {
                    target = item.Value as TComponent;
                    property.objectReferenceValue = target;
                }

                property.serializedObject.ApplyModifiedProperties();
                onClick?.Invoke(target);
            }

            SearchableComponentDropdownList<TComponent>(propertyName, property, menu, OnClick);
        }

        public static void SearchableComponentDropdownList<TComponent>(string propertyName, SerializedProperty property, SearchableDropdownItem root, Action<SearchableDropdownItem> onClick = null) where TComponent : Component
        {
            using (new ColorScope(EditorStyle.ErrorColor, () => property.objectReferenceValue == null))
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(propertyName, GUILayout.Width(EditorGUIUtility.labelWidth));
                property.objectReferenceValue = EditorGUILayout.ObjectField(property.objectReferenceValue, typeof(TComponent), true);
                var btnRect = GUILayoutUtility.GetLastRect();
                var btnType = GUILayout.Button("▽", EditorStyles.popup, GUILayout.Width(EditorGUIUtility.singleLineHeight));
                if (btnType)
                {
                    btnRect.width = EditorGUIUtility.currentViewWidth;
                    var dropdown = new SearchableDropdown(root, onClick);
                    dropdown.Show(btnRect, 300f);
                }

                GUILayout.EndHorizontal();
            }
        }

        #endregion

        #region Type Property Menu

        public static GenericMenu CreatePropertyMenu(Type type, SerializedProperty property, Action<PropertyInfo> onClickProperty = null, Action<FieldInfo> onClickField = null)
        {
            var menu = new GenericMenu();
            menu.AddItem(new GUIContent(EditorStyle.NoneStr), string.IsNullOrEmpty(property.stringValue), () =>
            {
                property.stringValue = "";
                property.serializedObject.ApplyModifiedProperties();
            });
            menu.AddSeparator("");

            var propertyInfos = TypeCaches.GetTypeProperties(type);
            var prefix = "Property/";
            foreach (var propertyInfo in propertyInfos)
            {
                // if (!TypeCaches.BindableTypes.Contains(propertyInfo.PropertyType)) continue;
                var displayName = propertyInfo.Name + "\t\t" + propertyInfo.PropertyType.Name;
                menu.AddItem(new GUIContent(prefix + displayName), propertyInfo.Name == property.stringValue, () =>
                {
                    property.stringValue = propertyInfo.Name;
                    property.serializedObject.ApplyModifiedProperties();
                    onClickProperty?.Invoke(propertyInfo);
                });
            }

            var filedInfos = TypeCaches.GetTypeFields(type);
            prefix = "Field/";
            foreach (var fieldInfo in filedInfos)
            {
                // if (!TypeCaches.BindableTypes.Contains(fieldInfo.FieldType)) continue;
                var displayName = fieldInfo.Name + "\t\t" + fieldInfo.FieldType.Name;
                menu.AddItem(new GUIContent(prefix + displayName), fieldInfo.Name == property.stringValue, () =>
                {
                    property.stringValue = fieldInfo.Name;
                    property.serializedObject.ApplyModifiedProperties();
                    onClickField?.Invoke(fieldInfo);
                });
            }

            return menu;
        }

        public static void PropertyTreeMenu(string propertyName, Type type, SerializedProperty property, Action<PropertyInfo> onClickProperty = null, Action<FieldInfo> onClickField = null)
        {
            var menu = CreatePropertyMenu(type, property, onClickProperty, onClickField);
            bool CheckNullFunc() => !TypeCaches.CheckTypeHasPropertyOrFieldByName(type, property.stringValue);
            void ResetFunc() => property.stringValue = null;

            (string currentPropertyName, string currentPropertyTypeName) GetCurrentPropertyInfo()
            {
                if (string.IsNullOrEmpty(property.stringValue)) return (EditorStyle.NoneStr, "");
                var (propertyInfo, filedInfo) = TypeCaches.GetTypePropertyOrFieldByName(type, property.stringValue);
                if (propertyInfo != null)
                {
                    return (propertyInfo.Name, propertyInfo.PropertyType.Name);
                }

                if (filedInfo != null)
                {
                    return (filedInfo.Name, filedInfo.FieldType.Name);
                }

                return (EditorStyle.NoneStr, "");
            }

            var (currentPropertyName, currentPropertyTypeName) = GetCurrentPropertyInfo();

            string CurrentDisplayNameGetter()
            {
                return currentPropertyName;
            }

            if (!string.IsNullOrEmpty(currentPropertyTypeName))
            {
                currentPropertyTypeName = " <color=#888888>(" + currentPropertyTypeName + ")</color>";
            }

            var displayPropertyName = propertyName + currentPropertyTypeName;
            DropdownList(displayPropertyName, menu, CheckNullFunc, ResetFunc, CurrentDisplayNameGetter);
        }

        #endregion

        #region Dropdown

        public static void DropdownList(string propertyName, GenericMenu menu, Func<bool> checkNullFunc, Action resetFunc, Func<string> currentDisplayNameGetter)
        {
            var isNull = checkNullFunc();
            if (isNull)
            {
                resetFunc();
            }
            
            using (new ColorScope(EditorStyle.ErrorColor, () => isNull))
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(propertyName, EditorStyle.RichLabel, GUILayout.Width(EditorGUIUtility.labelWidth));

                var displayName = currentDisplayNameGetter();
                var currentPropertyDisplayName = isNull ? EditorStyle.NoneStr : displayName;
                var btnProperty = GUILayout.Button(currentPropertyDisplayName, EditorStyles.popup);
                if (btnProperty)
                {
                    menu.ShowAsContext();
                }

                GUILayout.EndHorizontal();
            }
        }

        public static void SearchableDropdownList(string propertyName, SearchableDropdownItem root, Func<bool> checkNullFunc, Action resetFunc, Func<string> currentDisplayNameGetter, Action<SearchableDropdownItem> onClick = null)
        {
            var isNull = checkNullFunc();
            if (isNull)
            {
                resetFunc();
            }

            using (new ColorScope(EditorStyle.ErrorColor, () => isNull))
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(propertyName, GUILayout.Width(EditorGUIUtility.labelWidth));

                var displayName = currentDisplayNameGetter();
                var currentPropertyDisplayName = isNull ? EditorStyle.NoneStr : displayName;
                var btnRect = GUILayoutUtility.GetRect(new GUIContent(currentPropertyDisplayName), EditorStyles.toolbarButton);
                var btnType = GUI.Button(btnRect, displayName, EditorStyles.popup);
                if (btnType)
                {
                    btnRect.width = EditorGUIUtility.currentViewWidth;
                    var dropdown = new SearchableDropdown(root, onClick);
                    dropdown.Show(btnRect, 500f);
                }

                GUILayout.EndHorizontal();
            }
        }

        #endregion

        #region Color Line
        
        public static void ColorLine(Color color, float height)
        {
            GUILayout.BeginVertical();
            GUILayout.Space(height);
            GUILayout.EndVertical();
            var rect = GUILayoutUtility.GetLastRect();
            EditorGUI.DrawRect(rect, color);
        } 

        #endregion
    }
}
#endif