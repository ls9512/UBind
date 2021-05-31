using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aya.DataBinding
{
    [AddComponentMenu("Data Binding/Dropdown Binder")]
    public class DropdownBinder : ComponentTriggerBinder<Dropdown, int, RuntimeDropdownBinder>
    {
        public override Action AddListener => () => Target.onValueChanged.AddListener(OnValueChanged);
        public override Action RemoveListener => () => Target.onValueChanged.RemoveListener(OnValueChanged);
    }

    public class RuntimeDropdownBinder : DataBinder<Dropdown, int>
    {
        public override void SetData(int data)
        {
            Target.value = data;
        }

        public override int GetData()
        {
            return Target.value;
        }
    }

#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(DropdownBinder)), UnityEditor.CanEditMultipleObjects]
    public class DropdownBinderEditor : ComponentBinderEditor<Dropdown, int, RuntimeDropdownBinder>
    {
    }

#endif
}