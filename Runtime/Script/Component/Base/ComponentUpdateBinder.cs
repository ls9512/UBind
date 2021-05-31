using UnityEngine;

namespace Aya.DataBinding
{
    public abstract class ComponentUpdateBinder<TComponent, TValue, TDataBinder> : ComponentBinder<TComponent, TValue, TDataBinder>
        where TDataBinder : DataBinder<TComponent, TValue>, new()
        where TComponent : Component
    {
        public override bool NeedUpdate => true;

        public override void Update()
        {
            if (UpdateType != UpdateType.Update) return;
            DataBinder.UpdateSource();
        }

        public override void LateUpdate()
        {
            if (UpdateType != UpdateType.LateUpdate) return;
            DataBinder.UpdateSource();
        }

        public override void FixedUpdate()
        {
            if (UpdateType != UpdateType.FixedUpdate) return;
            DataBinder.UpdateSource();
        }
    }
}