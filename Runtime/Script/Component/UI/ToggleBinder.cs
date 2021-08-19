using UnityEngine;
using UnityEngine.UI;

namespace Aya.DataBinding
{
    [AddComponentMenu("Data Binding/Toggle Binder")]
    public class ToggleBinder : ComponentBinder<Toggle, bool, ToggleDataBinder>
    {

    }

    public class ToggleDataBinder : DataBinder<Toggle, bool>
    {
        public override void AddListener() => Target.onValueChanged.AddListener(OnValueChangedCallback);
        public override void RemoveListener() => Target.onValueChanged.RemoveListener(OnValueChangedCallback);

        public override bool Value
        {
            get => Target.isOn;
            set => Target.isOn = value;
        }
    }

#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(ToggleBinder)), UnityEditor.CanEditMultipleObjects]
    public class ToggleBinderEditor : ComponentBinderEditor<Toggle, bool, ToggleDataBinder>
    {
    }

#endif
}