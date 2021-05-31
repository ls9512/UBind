using System;

namespace Aya.DataBinding
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class BindValueBothAttribute : BindValueAttribute
    {
        public BindValueBothAttribute(string dataKey) : base(dataKey, DataDirection.Both)
        {
        }

        public BindValueBothAttribute(string contextKey, string dataKey) : base(contextKey, dataKey, DataDirection.Both)
        {
        }
    }
}