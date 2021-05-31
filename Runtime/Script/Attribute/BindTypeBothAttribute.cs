using System;

namespace Aya.DataBinding
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class BindTypeSourceAttribute : BindTypeAttribute
    {
        public BindTypeSourceAttribute(string dataKey) : base(dataKey, DataDirection.Source)
        {
        }

        public BindTypeSourceAttribute(string contextKey, string dataKey) : base(contextKey, dataKey, DataDirection.Source)
        {
        }
    }
}