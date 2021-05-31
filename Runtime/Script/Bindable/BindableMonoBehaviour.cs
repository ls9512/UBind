using UnityEngine;

namespace Aya.DataBinding
{
    public abstract class BindableMonoBehaviour : MonoBehaviour
    {
        public BindMap BindMap
        {
            get
            {
                if (_bindMap != null) return _bindMap;
                _bindMap = BindMap.GetBindMap(this);
                return _bindMap;
            }
        }

        private BindMap _bindMap;

        public virtual void OnEnable()
        {
            BindMap.Bind(this);
        }

        public virtual void OnDisable()
        {
            BindMap.UnBind(this);
        }
    }
}
