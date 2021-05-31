using System;

namespace Aya.DataBinding
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public abstract class BindAttribute : Attribute
    {
        public string Context = DataContext.Default;
        public string Key;
        public DataDirection Direction;

        protected BindAttribute(string dataKey, DataDirection direction)
        {
            Key = dataKey;
            Direction = direction;
        }

        protected BindAttribute(string contextKey, string dataKey, DataDirection direction)
        {
            Context = contextKey;
            Key = dataKey;
            Direction = direction;
        }
    }
}