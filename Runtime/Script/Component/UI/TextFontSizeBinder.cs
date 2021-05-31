using UnityEngine;
using UnityEngine.UI;

namespace Aya.DataBinding
{
    [AddComponentMenu("Data Binding/Text FontSize Binder")]
    public class TextFontSizeBinder : ComponentUpdateBinder<Text, int, RuntimeTextFontSizeBinder>
    {
    }

    public class RuntimeTextFontSizeBinder : DataBinder<Text, int>
    {
        public override void SetData(int data)
        {
            Target.fontSize = data;
        }

        public override int GetData()
        {
            return Target.fontSize;
        }
    }

#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(TextFontSizeBinder)), UnityEditor.CanEditMultipleObjects]
    public class TextFontSizeBinderEditor : ComponentBinderEditor<Text, int, RuntimeTextFontSizeBinder>
    {
    }

#endif
}