using System;

namespace Aya.DataBinding
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class BindTypeTargetAttribute : BindTypeAttribute
    {
        public BindTypeTargetAttribute(string dataKey) : base(dataKey, DataDirection.Target)
        {
        }

        public BindTypeTargetAttribute(string contextKey, string dataKey) : base(contextKey, dataKey, DataDirection.Target)
        {
        }
    }
}