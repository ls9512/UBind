using System;

namespace Aya.DataBinding
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class BindTypeAttribute : BindAttribute
    {
        public BindTypeAttribute(string dataKey, DataDirection direction) : base(dataKey, direction)
        {
        }

        public BindTypeAttribute(string containerKey, string dataKey, DataDirection direction) : base(containerKey, dataKey, direction)
        {
        }
    }
}