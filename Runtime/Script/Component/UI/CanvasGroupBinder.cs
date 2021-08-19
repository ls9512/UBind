using UnityEngine;

namespace Aya.DataBinding
{
    [AddComponentMenu("Data Binding/CanvasGroup Binder")]
    public class CanvasGroupBinder : ComponentBinder<CanvasGroup, float, RuntimeCanvasGroupBinder>
    {
        public override bool NeedUpdate => true;
    }

    public class RuntimeCanvasGroupBinder : DataBinder<CanvasGroup, float>
    {
        public override bool NeedUpdate => true;

        public override float Value
        {
            get => Target.alpha;
            set => Target.alpha = value;
        }
    }

#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(CanvasGroupBinder)), UnityEditor.CanEditMultipleObjects]
    public class CanvasGroupBinderEditor : ComponentBinderEditor<CanvasGroup, float, RuntimeCanvasGroupBinder>
    {
    }

#endif
}