using System;

namespace Aya.DataBinding
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class BindValueTargetAttribute : BindValueAttribute
    {
        public BindValueTargetAttribute(string dataKey) : base(dataKey, DataDirection.Target)
        {
        }

        public BindValueTargetAttribute(string contextKey, string dataKey) : base(contextKey, dataKey, DataDirection.Target)
        {
        }
    }
}