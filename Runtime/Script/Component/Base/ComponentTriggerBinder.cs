using System;
using UnityEngine;

namespace Aya.DataBinding
{
    public abstract class ComponentTriggerBinder<TComponent, TValue, TDataBinder> : ComponentBinder<TComponent, TValue, TDataBinder>
        where TDataBinder : DataBinder<TComponent, TValue>, new()
        where TComponent : Component
    {
        public override bool NeedUpdate => false;
        public abstract Action AddListener { get; }
        public abstract Action RemoveListener { get; }

        public override void OnEnable()
        {
            base.OnEnable();
            AddListener();
        }

        public override void OnDisable()
        {
            base.OnDisable();
            RemoveListener();
        }

        public virtual void OnValueChanged(TValue data)
        {
            DataBinder.UpdateSource();
        }
    }
}