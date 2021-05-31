using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aya.DataBinding
{
    [AddComponentMenu("Data Binding/Slider Binder")]
    public class SliderBinder : ComponentTriggerBinder<Slider, float, RuntimeSliderBinder>
    {
        public override Action AddListener => () => Target.onValueChanged.AddListener(OnValueChanged);
        public override Action RemoveListener => () => Target.onValueChanged.RemoveListener(OnValueChanged);
    }

    public class RuntimeSliderBinder : DataBinder<Slider, float>
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

    [UnityEditor.CustomEditor(typeof(SliderBinder)), UnityEditor.CanEditMultipleObjects]
    public class SliderBinderEditor : ComponentBinderEditor<Slider, float, RuntimeSliderBinder>
    {
    }

#endif
}