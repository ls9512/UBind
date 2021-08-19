using UnityEngine;
using UnityEngine.UI;

namespace Aya.DataBinding
{
    [AddComponentMenu("Data Binding/Text Binder")]
    public class TextBinder : ComponentBinder<Text, string, RuntimeTextBinder>
    {
        public override bool NeedUpdate => true;
    }

    public class RuntimeTextBinder : DataBinder<Text, string>
    {
        public override bool NeedUpdate => true;

        public override string Value
        {
            get => Target.text;
            set => Target.text = value;
        }
    }

#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(TextBinder)), UnityEditor.CanEditMultipleObjects]
    public class TextBinderEditor : ComponentBinderEditor<Text, string, RuntimeTextBinder>
    {
    }

#endif
}