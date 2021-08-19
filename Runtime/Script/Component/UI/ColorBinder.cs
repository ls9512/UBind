using UnityEngine;
using UnityEngine.UI;

namespace Aya.DataBinding
{
    [AddComponentMenu("Data Binding/Color Binder")]
    public class ColorBinder : ComponentBinder<Graphic, Color, RuntimeColorBinder>
    {
        public override bool NeedUpdate => true;
    }

    public class RuntimeColorBinder : DataBinder<Graphic, Color>
    {
        public override bool NeedUpdate => true;

        public override Color Value
        {
            get => Target.color;
            set => Target.color = value;
        }
    }

#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(ColorBinder)), UnityEditor.CanEditMultipleObjects]
    public class ColorBinderEditor : ComponentBinderEditor<Graphic, Color, RuntimeColorBinder>
    {
    }

#endif
}