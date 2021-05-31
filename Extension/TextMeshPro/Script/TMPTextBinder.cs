#if UBIND_TEXTMESHPRO
using TMPro;
using UnityEngine;

namespace Aya.DataBinding
{
    [AddComponentMenu("Data Binding/TMP Text Binder")]
    public class TMPTextBinder : ComponentUpdateBinder<TextMeshPro, string, RuntimeTMPTextBinder>
    {
    }

    public class RuntimeTMPTextBinder : DataBinder<TextMeshPro, string>
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

    [UnityEditor.CustomEditor(typeof(TMPTextBinder)), UnityEditor.CanEditMultipleObjects]
    public class TMPTextBinderEditor : ComponentBinderEditor<TextMeshPro, string, RuntimeTMPTextBinder>
    {
    }

#endif
}
#endif