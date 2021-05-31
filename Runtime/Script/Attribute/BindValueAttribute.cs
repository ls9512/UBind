using System;

namespace Aya.DataBinding
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class BindValueAttribute : BindAttribute
    {
        public BindValueAttribute(string dataKey, DataDirection direction) : base(dataKey, direction)
        {
        }

        public BindValueAttribute(string contextKey, string dataKey, DataDirection direction) : base(contextKey, dataKey, direction)
        {
        }
    }
}