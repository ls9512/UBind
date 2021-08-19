using UnityEngine;
using UnityEngine.UI;

namespace Aya.DataBinding
{
    [AddComponentMenu("Data Binding/Image FillAmount Binder")]
    public class ImageFillAmountBinder : ComponentBinder<Image, float, RuntimeImageFillAmountBinder>
    {
        public override bool NeedUpdate => true;
    }

    public class RuntimeImageFillAmountBinder : DataBinder<Image, float>
    {
        public override bool NeedUpdate => true;

        public override float Value
        {
            get => Target.fillAmount;
            set => Target.fillAmount = value;
        }
    }

#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(ImageFillAmountBinder)), UnityEditor.CanEditMultipleObjects]
    public class ImageFillAmountBinderEditor : ComponentBinderEditor<Image, float, RuntimeImageFillAmountBinder>
    {
    }

#endif
}