using UnityEngine;
using UnityEngine.UI;

namespace Aya.DataBinding
{
    [AddComponentMenu("Data Binding/Slider Binder")]
    public class SliderBinder : ComponentBinder<Slider, float, RuntimeSliderBinder>
    {
        
    }

    public class RuntimeSliderBinder : DataBinder<Slider, float>
    {
        public override void AddListener() => Target.onValueChanged.AddListener(OnValueChangedCallback);
        public override void RemoveListener() => Target.onValueChanged.RemoveListener(OnValueChangedCallback);

        public override float Value
        {
            get => Target.value;
            set => Target.value = value;
        }
    }

#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(SliderBinder)), UnityEditor.CanEditMultipleObjects]
    public class SliderBinderEditor : ComponentBinderEditor<Slider, float, RuntimeSliderBinder>
    {
    }

#endif
}