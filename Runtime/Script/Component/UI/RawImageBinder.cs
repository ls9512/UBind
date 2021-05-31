using UnityEngine;
using UnityEngine.UI;

namespace Aya.DataBinding
{
    [AddComponentMenu("Data Binding/RawImage Binder")]
    public class RawImageBinder : ComponentUpdateBinder<RawImage, Texture, RuntimeRawImageBinder>
    {
    }

    public class RuntimeRawImageBinder : DataBinder<RawImage, Texture>
    {
        public override void SetData(Texture data)
        {
            Target.texture = data;
        }

        public override Texture GetData()
        {
            return Target.texture;
        }
    }

#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(RawImageBinder)), UnityEditor.CanEditMultipleObjects]
    public class RawImageBinderEditor : ComponentBinderEditor<RawImage, Texture, RuntimeRawImageBinder>
    {
    }

#endif
}