#if UNITY_EDITOR
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Aya.DataBinding
{
    public static class GUIUtil
    {
        #region Assembly & Type

        public static SearchableDropdownItem CreateAssemblyMenu()
        {
            var root = new SearchableDropdownItem("Assembly");
            root.AddChild(new SearchableDropdownItem(EditorStyle.NoneStr, null));
            root.AddSeparator();

            var assemblies = TypeCaches.Assemblies;
            for (var i = 0; i < assemblies.Length; i++)
            {
                var assembly = assemblies[i];
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
            if (assembly == null) return root;
            var types = assembly.GetTypes();
            for (var i = 0; i < types.Length; i++)
            {
                var type = types[i];
                if (type.IsAbstract) continue;
                if (type.IsInterface) continue;
                if (type.IsGenericType) continue;
                if (type.IsEnum) continue;

                var child = new SearchableDropdownItem(type.Name, type.FullName);
                root.AddChild(child);
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

        #region Enum

        public static void DrawToolbarEnum(SerializedProperty property, Type enumType)
        {
            DrawToolbarEnum(property, property.displayName, enumType);
        }

        public static void DrawToolbarEnum(SerializedProperty property, string propertyName, Type enumType)
        {
            property.intValue = DrawToolbarEnum(property.intValue, propertyName, enumType);
        }

        public static int DrawToolbarEnum(int value, string propertyName, Type enumType)
        {
            using (GUIHorizontal.Create())
            {
                GUILayout.Label(propertyName, EditorStyles.label, GUILayout.Width(EditorGUIUtility.labelWidth));
                var buttons = Enum.GetNames(enumType);
                var style = EditorStyles.miniButton;
                style.margin = new RectOffset();
                var rect = EditorGUILayout.GetControlRect(false, EditorGUIUtility.singleLineHeight, style);
                var btnWidth = rect.width / buttons.Length;
                for (var i = 0; i < buttons.Length; i++)
                {
                    var button = buttons[i];
                    var index = i;
                    var btnRect = rect;
                    btnRect.x += i * btnWidth;
                    btnRect.width = btnWidth;
                    using (GUIColorArea.Create(Color.white, Color.gray * 1.5f, value == index))
                    {
                        var btn = GUI.Button(btnRect, button, style);
                        if (btn)
                        {
                            return index;
                        }
                    }
                }
            }

            return value;
        }

        #endregion

        #region Button
      
        public static bool DrawSearchMenuButton()
        {
            var button = DrawIconButton("Search Icon", "Search");
            return button;
        }

        public static bool DrawIconButton(string iconName, string toolTip = "")
        {
            var content = new GUIContent(EditorGUIUtility.IconContent(iconName))
            {
                tooltip = toolTip
            };

            var button = GUILayout.Button(content, EditorStyles.miniButtonMid, GUILayout.Width(EditorGUIUtility.singleLineHeight));
            return button;
        } 

        #endregion
    }
}
#endif