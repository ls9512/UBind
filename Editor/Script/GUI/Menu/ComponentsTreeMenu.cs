#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace Aya.DataBinding
{
    public static partial class GUIMenu
    {
        public static SearchableDropdownItem CreateComponentsTreeMenu<TComponent>(Transform parent) where TComponent : Component
        {
            return CreateComponentsTreeMenu(typeof(TComponent), parent);
        }

        public static SearchableDropdownItem CreateComponentsTreeMenu(Type componentType, Transform parent)
        {
            var root = new SearchableDropdownItem(componentType.Name);
            root.AddChild(new SearchableDropdownItem(EditorStyle.NoneStr, null));
            root.AddSeparator();
            CreateComponentsTreeMenuRecursion(componentType, root, parent, "");
            return root;
        }

        private static void CreateComponentsTreeMenuRecursion<TComponent>(SearchableDropdownItem root, Transform parent, string path) where TComponent : Component
        {
            CreateComponentsTreeMenuRecursion(typeof(TComponent), root, parent, path);
        }

        private static void CreateComponentsTreeMenuRecursion(Type componentType, SearchableDropdownItem root, Transform parent, string path)
        {
            var components = parent.GetComponents(componentType);
            foreach (var component in components)
            {
                var componentName = component.GetType().Name;
                var child = new SearchableDropdownItem(path + componentName, component)
                {
                    icon = EditorGUIUtility.ObjectContent(component, component.GetType()).image as Texture2D
                };

                root.AddChild(child);
            }

            if (parent.childCount == 0) return;
            if (components.Length > 0)
            {
                root.AddSeparator();
            }

            for (var i = 0; i < parent.childCount; i++)
            {
                var childTrans = parent.GetChild(i);
                CreateComponentsTreeMenuRecursion(componentType, root, childTrans, path + childTrans.name + " \\ ");
            }
        }

        public static void ComponentTreeMenu<TComponent>(string propertyName, SerializedProperty property, Transform parent, Action<TComponent> onClick = null) where TComponent : Component
        {
            ComponentTreeMenu(typeof(TComponent), propertyName, property, parent, obj =>
            {
                var target = (TComponent)obj;
                onClick?.Invoke(target);
            });
        }

        public static void ComponentTreeMenu(Type componentType, string propertyName, SerializedProperty property, Transform parent, Action<UnityEngine.Object> onClick = null)
        {
            var menu = parent != null ? CreateComponentsTreeMenu(componentType, parent) : null;

            void OnClick(SearchableDropdownItem item)
            {
                UnityEngine.Object target = null;
                if (item.Value == null)
                {
                    property.objectReferenceValue = null;
                }
                else
                {
                    target = (UnityEngine.Object)item.Value;
                    property.objectReferenceValue = target;
                }

                property.serializedObject.ApplyModifiedProperties();
                onClick?.Invoke(target);
            }

            SearchableComponentDropdownList(componentType, propertyName, property, menu, OnClick);
        }

        public static void SearchableComponentDropdownList<TComponent>(string propertyName, SerializedProperty property, SearchableDropdownItem root, Action<SearchableDropdownItem> onClick = null) where TComponent : Component
        {
            SearchableComponentDropdownList(typeof(TComponent), propertyName, property, root, onClick);
        }

        public static void SearchableComponentDropdownList(Type componentType, string propertyName, SerializedProperty property, SearchableDropdownItem root, Action<SearchableDropdownItem> onClick = null)
        {
            using (GUIErrorColorArea.Create(property.objectReferenceValue == null))
            {
                using (GUIHorizontal.Create())
                {
                    GUILayout.Label(propertyName, EditorStyles.label, GUILayout.Width(EditorGUIUtility.labelWidth));
                    property.objectReferenceValue = EditorGUILayout.ObjectField(property.objectReferenceValue, componentType, true);

                    if (root != null)
                    {
                        var btnRect = GUILayoutUtility.GetLastRect();
                        if (GUIUtil.DrawSearchMenuButton())
                        {
                            btnRect.width = EditorGUIUtility.currentViewWidth;
                            var dropdown = new SearchableDropdown(root, onClick);
                            dropdown.Show(btnRect, 500f);
                        }
                    }
                }
            }
        }
    }
}
#endif