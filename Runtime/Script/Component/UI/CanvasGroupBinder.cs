using UnityEngine;

namespace Aya.DataBinding
{
    [AddComponentMenu("Data Binding/CanvasGroup Binder")]
    public class CanvasGroupBinder : ComponentUpdateBinder<CanvasGroup, float, RuntimeCanvasGroupBinder>
    {
    }

    public class RuntimeCanvasGroupBinder : DataBinder<CanvasGroup, float>
    {
        public override void SetData(float data)
        {
            Target.alpha = data;
        }

        public override float GetData()
        {
            return Target.alpha;
        }
    }

#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(CanvasGroupBinder)), UnityEditor.CanEditMultipleObjects]
    public class CanvasGroupBinderEditor : ComponentBinderEditor<CanvasGroup, float, RuntimeCanvasGroupBinder>
    {
    }

#endif
}