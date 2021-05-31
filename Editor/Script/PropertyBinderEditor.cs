#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Aya.DataBinding
{
    [CustomEditor(typeof(PropertyBinder)), CanEditMultipleObjects]
    public class PropertyBinderEditor : ComponentBinderEditor<Component, object, RuntimePropertyBinder<Component>>
    {
        public PropertyBinder PropertyBinder => target as PropertyBinder;

        protected SerializedProperty PropertyNameProperty;

        public override void OnEnable()
        {
            base.OnEnable();
            PropertyNameProperty = serializedObject.FindProperty("Property");
        }

        public override void DrawBody()
        {
            if (PropertyBinder.Target == null)
            {
                PropertyNameProperty.stringValue = "";
                return;
            }

            var type = PropertyBinder.Target.GetType();
            EditorUtil.PropertyTreeMenu("Dst Property", type, PropertyNameProperty);
        }
    }
}
#endif