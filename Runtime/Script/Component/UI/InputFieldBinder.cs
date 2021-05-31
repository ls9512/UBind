using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aya.DataBinding
{
    [AddComponentMenu("Data Binding/InputField Binder")]
    public class InputFieldBinder : ComponentTriggerBinder<InputField, string, RuntimeInputDataBinder>
    {
        public override Action AddListener => () => Target.onValueChanged.AddListener(OnValueChanged);
        public override Action RemoveListener => () => Target.onValueChanged.RemoveListener(OnValueChanged);
    }

    public class RuntimeInputDataBinder : DataBinder<InputField, string>
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

    [UnityEditor.CustomEditor(typeof(InputFieldBinder)), UnityEditor.CanEditMultipleObjects]
    public class InputFieldBinderEditor : ComponentBinderEditor<InputField, string, RuntimeInputDataBinder>
    {
    }

#endif
}