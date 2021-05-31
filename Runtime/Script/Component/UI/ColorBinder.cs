using UnityEngine;
using UnityEngine.UI;

namespace Aya.DataBinding
{
    [AddComponentMenu("Data Binding/Color Binder")]
    public class ColorBinder : ComponentUpdateBinder<Graphic, Color, RuntimeColorBinder>
    {
    }

    public class RuntimeColorBinder : DataBinder<Graphic, Color>
    {
        public override void SetData(Color data)
        {
            Target.color = data;
        }

        public override Color GetData()
        {
            return Target.color;
        }
    }

#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(ColorBinder)), UnityEditor.CanEditMultipleObjects]
    public class ColorBinderEditor : ComponentBinderEditor<Graphic, Color, RuntimeColorBinder>
    {
    }

#endif
}