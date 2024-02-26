using System;

namespace Aya.DataBinding
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class BindTypeBothAttribute : BindTypeAttribute
    {
        public BindTypeBothAttribute(string dataKey) : base(dataKey, DataDirection.Both)
        {
        }

        public BindTypeBothAttribute(string containerKey, string dataKey) : base(containerKey, dataKey, DataDirection.Both)
        {
        }
    }
}