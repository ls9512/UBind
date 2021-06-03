using Aya.DataBinding;
using UnityEngine;

namespace Aya.Sample
{
    public class BindableMonoBehaviourTest : BindableMonoBehaviour
    {
        [Header("Runtime Value Binder Test")]
        [BindValueSource("Value")]
        public string AutoSrc;

        [BindValueTarget("Value")]
        public string AutoDst;

        public string ManualDst;

        private DataBinder _runtimeValueBinder;

        public override void OnEnable()
        {
            base.OnEnable();
            _runtimeValueBinder = UBind.BindTarget<string>("Value", str => ManualDst = str);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            _runtimeValueBinder.UnBind();
            _runtimeValueBinder = null;
        }

        public void Update()
        {
        }
    }
}