#if UBIND_TEXTMESHPRO
using TMPro;
using UnityEngine;

namespace Aya.DataBinding
{
    [AddComponentMenu("Data Binding/TMP Input Field Binder")]
    public class TMPInputFieldBinder : ComponentBinder<TMP_InputField, string, RuntimeTMPInputFieldBinder>
    {
    }

    public class RuntimeTMPInputFieldBinder : DataBinder<TMP_InputField, string>
    {
        public override void AddListener() => Target.onValueChanged.AddListener(OnValueChangedCallback);
        public override void RemoveListener() => Target.onValueChanged.RemoveListener(OnValueChangedCallback);

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

    [UnityEditor.CustomEditor(typeof(TMPInputFieldBinder)), UnityEditor.CanEditMultipleObjects]
    public class TMPInputFieldBinderEditor : ComponentBinderEditor<TMP_InputField, string, RuntimeTMPInputFieldBinder>
    {
    }

#endif
}
#endif