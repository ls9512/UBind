#if UBIND_TEXTMESHPRO
using TMPro;
using UnityEngine;

namespace Aya.DataBinding
{
    [AddComponentMenu("Data Binding/TMP Text Binder")]
    public class TMPTextBinder : ComponentBinder<TMP_Text, string, RuntimeTMPTextBinder>
    {
        public override bool NeedUpdate => true;
    }

    public class RuntimeTMPTextBinder : DataBinder<TMP_Text, string>
    {
        public override bool NeedUpdate => true;

        public override string Value
        {
            get => Target.text;
            set => Target.text = value;
        }
    }

#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(TMPTextBinder)), UnityEditor.CanEditMultipleObjects]
    public class TMPTextBinderEditor : ComponentBinderEditor<TMP_Text, string, RuntimeTMPTextBinder>
    {
    }

#endif
}
#endif