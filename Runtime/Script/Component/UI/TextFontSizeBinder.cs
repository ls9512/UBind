using UnityEngine;
using UnityEngine.UI;

namespace Aya.DataBinding
{
    [AddComponentMenu("Data Binding/Text FontSize Binder")]
    public class TextFontSizeBinder : ComponentBinder<Text, int, RuntimeTextFontSizeBinder>
    {
        public override bool NeedUpdate => true;
    }

    public class RuntimeTextFontSizeBinder : DataBinder<Text, int>
    {
        public override bool NeedUpdate => true;

        public override int Value
        {
            get => Target.fontSize;
            set => Target.fontSize = value;
        }
    }

#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(TextFontSizeBinder)), UnityEditor.CanEditMultipleObjects]
    public class TextFontSizeBinderEditor : ComponentBinderEditor<Text, int, RuntimeTextFontSizeBinder>
    {
    }

#endif
}