#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Aya.DataBinding
{
    public class BaseBinderEditor : Editor
    {
        protected void DrawContainerKey(SerializedProperty property)
        {
            if (property.stringValue == DataContainer.Default)
            {
                using (new ColorScope(Color.gray))
                {
                    EditorGUILayout.PropertyField(property);
                }
            }
            else
            {
                using (new ColorScope(EditorStyle.ErrorColor, () => string.IsNullOrEmpty(property.stringValue)))
                {
                    EditorGUILayout.PropertyField(property);
                }
            }
        }

        protected void DrawDataKey(SerializedProperty property)
        {
            using (new ColorScope(EditorStyle.ErrorColor, () => string.IsNullOrEmpty(property.stringValue)))
            {
                EditorGUILayout.PropertyField(property);
            }
        }

        protected void DrawDirection(SerializedProperty property)
        {
            GUIUtil.DrawToolbarEnum(property, typeof(DataDirection));
        }

        protected void DrawTarget<TComponent>(SerializedProperty property, Transform root) where TComponent : Component
        {
            GUIMenu.ComponentTreeMenu<TComponent>("Target", property, root);
        }

        public static void DrawTargetAndProperty<TComponent>(string targetPropertyName, SerializedProperty targetProperty, Transform parent, string propertyName, SerializedProperty property) where TComponent : Component
        {
            var originalTarget = targetProperty.objectReferenceValue;
            GUIMenu.ComponentTreeMenu<TComponent>(targetPropertyName, targetProperty, parent, component =>
            {
                // Auto Bind
                var change = originalTarget != targetProperty.objectReferenceValue && targetProperty.objectReferenceValue != null;
                if (!change) return;
                if (TypeCaches.AutoBindComponentTypeDic.TryGetValue(component.GetType(), out var autoBindPropertyName))
                {
                    property.stringValue = autoBindPropertyName;
                    property.serializedObject.ApplyModifiedProperties();
                }
            });

            if (targetProperty.objectReferenceValue == null && !string.IsNullOrEmpty(property.stringValue))
            {
                property.stringValue = "";
            }

            if (targetProperty.objectReferenceValue != null)
            {
                var type = targetProperty.objectReferenceValue.GetType();
                GUIMenu.DrawPropertyMenu(null, targetProperty.objectReferenceValue.GetType(), propertyName, property);
            }
        }
    }
}
#endif