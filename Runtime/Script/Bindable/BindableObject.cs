
namespace Aya.DataBinding
{
    public abstract class BindableObject
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

        protected BindableObject()
        {
            BindMap.Bind(this);
        }

        ~BindableObject()
        {
            BindMap.UnBind(this);
        }
    }
}
