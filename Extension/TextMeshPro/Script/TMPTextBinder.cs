#if UBIND_TEXTMESHPRO
using TMPro;
using UnityEngine;

namespace Aya.DataBinding
{
    [AddComponentMenu("Data Binding/TMP Text Binder")]
    public class TMPTextBinder : ComponentBinder<TextMeshPro, string, RuntimeTMPTextBinder>
    {
        public override bool NeedUpdate => true;
    }

    public class RuntimeTMPTextBinder : DataBinder<TextMeshPro, string>
    {
        public override bool NeedUpdate => true;

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