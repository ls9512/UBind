using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aya.DataBinding
{
    [AddComponentMenu("Data Binding/Scrollbar Binder")]
    public class ScrollbarBinder : ComponentTriggerBinder<Scrollbar, float, RuntimeScrollbarBinder>
    {
        public override Action AddListener => () => Target.onValueChanged.AddListener(OnValueChanged);
        public override Action RemoveListener => () => Target.onValueChanged.RemoveListener(OnValueChanged);
    }

    public class RuntimeScrollbarBinder : DataBinder<Scrollbar, float>
    {
        public override void SetData(float data)
        {
            Target.value = data;
        }

        public override float GetData()
        {
            return Target.value;
        }
    }

#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(ScrollbarBinder)), UnityEditor.CanEditMultipleObjects]
    public class ScrollbarBinderEditor : ComponentBinderEditor<Scrollbar, float, RuntimeScrollbarBinder>
    {
    }

#endif
}