#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Aya.DataBinding
{
    public static class AdvancedDropdownExtensions
    {
        public static void Show(this AdvancedDropdown dropdown, Rect buttonRect, float maxHeight)
        {
            dropdown.Show(buttonRect);
            SetMaxHeightForOpenedPopup(buttonRect, maxHeight);
        }

        private static void SetMaxHeightForOpenedPopup(Rect buttonRect, float maxHeight)
        {
            var window = EditorWindow.focusedWindow;

            if (window == null)
            {
                Debug.LogWarning("EditorWindow.focusedWindow was null.");
                return;
            }

            if (!string.Equals(window.GetType().Namespace, typeof(AdvancedDropdown).Namespace))
            {
                Debug.LogWarning("EditorWindow.focusedWindow " + EditorWindow.focusedWindow.GetType().FullName + " was not in expected namespace.");
                return;
            }

            var position = window.position;
            if (position.height <= maxHeight)
            {
                return;
            }

            position.height = maxHeight;
            window.minSize = position.size;
            window.maxSize = position.size;
            window.position = position;
            window.ShowAsDropDown(GUIUtility.GUIToScreenRect(buttonRect), position.size);
        }
    }
}
#endif