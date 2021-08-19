using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.DataBinding
{
    [AddComponentMenu("Data Binding/Text Format Value Binder")]
    public class TextFormatValueBinder : ComponentBinder<Text, float, RuntimeTextFormatValueBinder>
    {
        public override bool NeedUpdate => true;
        public string Text;

        public override RuntimeTextFormatValueBinder CreateDataBinder()
        {
            var dataBinder = new RuntimeTextFormatValueBinder
            {
                Target = Target,
                Context = Context,
                Direction = Direction,
                Key = Key,
                Text = Text
            };

            return dataBinder;
        }

        public override void Reset()
        {
            Text = "{0:F2}";
        }
    }

    public class RuntimeTextFormatValueBinder : DataBinder<Text, float>
    {
        public override bool NeedUpdate => true;
        public string Text;

        public override float Value
        {
            get => _value;
            set
            {
                _value = value;
                var str = string.Format(Text, _value);
                Target.text = str;
            }
        }

        private float _value;
    }

#if UNITY_EDITOR

    [CustomEditor(typeof(TextFormatValueBinder)), CanEditMultipleObjects]
    public class TextFormatValueBinderEditor : ComponentBinderEditor<Text, float, RuntimeTextFormatValueBinder>
    {
        public TextFormatValueBinder Binder => target as TextFormatValueBinder;
        protected SerializedProperty TextProperty;

        public override void OnEnable()
        {
            base.OnEnable();
            TextProperty = serializedObject.FindProperty(nameof(Binder.Text));
        }

        public override void DrawBody()
        {
            EditorGUILayout.PropertyField(TextProperty);
        }
    }

#endif
}