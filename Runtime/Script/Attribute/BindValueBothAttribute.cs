using System;

namespace Aya.DataBinding
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class BindValueSourceAttribute : BindValueAttribute
    {
        public BindValueSourceAttribute(string dataKey) : base(dataKey, DataDirection.Source)
        {
        }

        public BindValueSourceAttribute(string contextKey, string dataKey) : base(contextKey, dataKey, DataDirection.Source)
        {
        }
    }
}