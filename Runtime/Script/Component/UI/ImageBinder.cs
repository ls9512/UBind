using UnityEngine;
using UnityEngine.UI;

namespace Aya.DataBinding
{
    [AddComponentMenu("Data Binding/Image Binder")]
    public class ImageBinder : ComponentUpdateBinder<Image, Sprite, RuntimeImageBinder>
    {
    }

    public class RuntimeImageBinder : DataBinder<Image, Sprite>
    {
        public override void SetData(Sprite data)
        {
            Target.sprite = data;
        }

        public override Sprite GetData()
        {
            return Target.sprite;
        }
    }

#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(ImageBinder)), UnityEditor.CanEditMultipleObjects]
    public class ImageBinderEditor : ComponentBinderEditor<Image, Sprite, RuntimeImageBinder>
    {
    }

#endif
}