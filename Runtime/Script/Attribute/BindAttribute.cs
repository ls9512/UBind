using System;

namespace Aya.DataBinding
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public abstract class BindAttribute : Attribute
    {
        public string Container = DataContainer.Default;
        public string Key;
        public DataDirection Direction;

        protected BindAttribute(string dataKey, DataDirection direction)
        {
            Key = dataKey;
            Direction = direction;
        }

        protected BindAttribute(string containerKey, string dataKey, DataDirection direction)
        {
            Container = containerKey;
            Key = dataKey;
            Direction = direction;
        }
    }
}