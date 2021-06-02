using UnityEngine;
using UnityEngine.UI;

namespace Aya.DataBinding
{
    [AddComponentMenu("Data Binding/RawImage Binder")]
    public class RawImageBinder : ComponentBinder<RawImage, Texture, RuntimeRawImageBinder>
    {
        public override bool NeedUpdate => true;
    }

    public class RuntimeRawImageBinder : DataBinder<RawImage, Texture>
    {
        public override bool NeedUpdate => true;

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