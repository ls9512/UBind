using System;

namespace Aya.DataBinding
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class BindTypeAttribute : BindAttribute
    {
        public BindTypeAttribute(string dataKey, DataDirection direction) : base(dataKey, direction)
        {
        }

        public BindTypeAttribute(string contextKey, string dataKey, DataDirection direction) : base(contextKey, dataKey, direction)
        {
        }
    }
}