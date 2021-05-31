using UnityEngine;
using UnityEngine.UI;

namespace Aya.DataBinding
{
    [AddComponentMenu("Data Binding/Text Binder")]
    public class TextBinder : ComponentUpdateBinder<Text, string, RuntimeTextBinder>
    {
    }

    public class RuntimeTextBinder : DataBinder<Text, string>
    {
        public override void SetData(string data)
        {
            Target.text = data;
        }

        public override string GetData()
        {
            return Target.text;
        }
    }

#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(TextBinder)), UnityEditor.CanEditMultipleObjects]
    public class TextBinderEditor : ComponentBinderEditor<Text, string, RuntimeTextBinder>
    {
    }

#endif
}