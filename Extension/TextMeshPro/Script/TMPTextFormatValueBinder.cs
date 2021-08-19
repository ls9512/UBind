using TMPro;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.DataBinding
{
    [AddComponentMenu("Data Binding/TMP Text Format Value Binder")]
    public class TMPTextFormatValueBinder : ComponentBinder<TMP_Text, float, RuntimeTMPTextFormatValueBinder>
    {
        public override bool NeedUpdate => true;
        public string Text;

        public override RuntimeTMPTextFormatValueBinder CreateDataBinder()
        {
            var dataBinder = new RuntimeTMPTextFormatValueBinder
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

    public class RuntimeTMPTextFormatValueBinder : DataBinder<TMP_Text, float>
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

    [CustomEditor(typeof(TMPTextFormatValueBinder)), CanEditMultipleObjects]
    public class TMPTextFormatValueBinderEditor : ComponentBinderEditor<TMP_Text, float, RuntimeTMPTextFormatValueBinder>
    {
        public TMPTextFormatValueBinder Binder => target as TMPTextFormatValueBinder;
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