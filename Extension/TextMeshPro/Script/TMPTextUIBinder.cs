#if UBIND_TEXTMESHPRO
using TMPro;
using UnityEngine;

namespace Aya.DataBinding
{
    [AddComponentMenu("Data Binding/TMP Text Binder (UI)")]
    public class TMPTextUIBinder : ComponentUpdateBinder<TextMeshProUGUI, string, RuntimeTMPTextUIBinder>
    {
    }

    public class RuntimeTMPTextUIBinder : DataBinder<TextMeshProUGUI, string>
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

    [UnityEditor.CustomEditor(typeof(TMPTextUIBinder)), UnityEditor.CanEditMultipleObjects]
    public class TMPTextUIBinderEditor : ComponentBinderEditor<TextMeshProUGUI, string, RuntimeTMPTextUIBinder>
    {
    }

#endif
}
#endif