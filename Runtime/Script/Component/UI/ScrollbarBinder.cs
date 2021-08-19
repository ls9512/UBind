using UnityEngine;
using UnityEngine.UI;

namespace Aya.DataBinding
{
    [AddComponentMenu("Data Binding/Scrollbar Binder")]
    public class ScrollbarBinder : ComponentBinder<Scrollbar, float, RuntimeScrollbarBinder>
    {
        
    }

    public class RuntimeScrollbarBinder : DataBinder<Scrollbar, float>
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

    [UnityEditor.CustomEditor(typeof(ScrollbarBinder)), UnityEditor.CanEditMultipleObjects]
    public class ScrollbarBinderEditor : ComponentBinderEditor<Scrollbar, float, RuntimeScrollbarBinder>
    {
    }

#endif
}