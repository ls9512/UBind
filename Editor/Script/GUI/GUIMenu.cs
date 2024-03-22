#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Aya.DataBinding
{
    public static partial class GUIMenu
    {
        public static void DrawSearchableDropdownMenu(string propertyName,
            SearchableDropdown menu, 
            Func<bool> checkNullFunc,
            Func<string> currentDisplayNameGetter)
        {
            var isNull = checkNullFunc();
            using (GUIErrorColorArea.Create(isNull))
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(propertyName, GUILayout.Width(EditorGUIUtility.labelWidth));

                var displayName = currentDisplayNameGetter();
                var currentPropertyDisplayName = isNull ? EditorStyle.NoneStr : displayName;
                var btnRect = GUILayoutUtility.GetRect(new GUIContent(currentPropertyDisplayName), EditorStyles.toolbarButton);
                var btnType = GUI.Button(btnRect, currentPropertyDisplayName, EditorStyles.popup);
                if (btnType)
                {
                    btnRect.width = EditorGUIUtility.currentViewWidth;
                    menu.Show(btnRect, 500f);
                }

                GUILayout.EndHorizontal();
            }
        }

        public static SearchableDropdown CreateSearchableDropdownMenu<T>(string menuTitle,
            IEnumerable<T> valueSource, 
            Func<T, string> pathGetter, 
            Func<T, Texture2D> iconGetter, 
            Action<T> onClick = null)
        {
            var root = new SearchableDropdownItem(menuTitle);
            var menu = new SearchableDropdown(root, item =>
            {
                var value = (T)item.Value;
                onClick?.Invoke(value);
            });

            root.AddChild(new SearchableDropdownItem(EditorStyle.NoneStr, null));
            root.AddSeparator();

            foreach (var value in valueSource)
            {
                var path = pathGetter(value);
                var icon = iconGetter(value);
                var item = new SearchableDropdownItem(path, value)
                {
                    icon = icon
                };

                root.AddChild(item);
            }

            return menu;
        }
    }
}
#endif