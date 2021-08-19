using UnityEngine;
using UnityEngine.UI;

namespace Aya.DataBinding
{
    [AddComponentMenu("Data Binding/InputField Binder")]
    public class InputFieldBinder : ComponentBinder<InputField, string, RuntimeInputDataBinder>
    {
       
    }

    public class RuntimeInputDataBinder : DataBinder<InputField, string>
    {
        public override void AddListener() => Target.onValueChanged.AddListener(OnValueChangedCallback);
        public override void RemoveListener() => Target.onValueChanged.RemoveListener(OnValueChangedCallback);

        public override string Value
        {
            get => Target.text;
            set => Target.text = value;
        }
    }

#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(InputFieldBinder)), UnityEditor.CanEditMultipleObjects]
    public class InputFieldBinderEditor : ComponentBinderEditor<InputField, string, RuntimeInputDataBinder>
    {
    }

#endif
}