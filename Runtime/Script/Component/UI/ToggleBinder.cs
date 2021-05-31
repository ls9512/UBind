using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aya.DataBinding
{
    [AddComponentMenu("Data Binding/Toggle Binder")]
    public class ToggleBinder : ComponentTriggerBinder<Toggle, bool, ToggleDataBinder>
    {
        public override Action AddListener => () => Target.onValueChanged.AddListener(OnValueChanged);
        public override Action RemoveListener => () => Target.onValueChanged.RemoveListener(OnValueChanged);
    }

    public class ToggleDataBinder : DataBinder<Toggle, bool>
    {
        public override void SetData(bool data)
        {
            Target.isOn = data;
        }

        public override bool GetData()
        {
            return Target.isOn;
        }
    }

#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(ToggleBinder)), UnityEditor.CanEditMultipleObjects]
    public class ToggleBinderEditor : ComponentBinderEditor<Toggle, bool, ToggleDataBinder>
    {
    }

#endif
}