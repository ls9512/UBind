using System;

namespace Aya.DataBinding
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class BindTypeBothAttribute : BindTypeAttribute
    {
        public BindTypeBothAttribute(string dataKey) : base(dataKey, DataDirection.Both)
        {
        }

        public BindTypeBothAttribute(string contextKey, string dataKey) : base(contextKey, dataKey, DataDirection.Both)
        {
        }
    }
}