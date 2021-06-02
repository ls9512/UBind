using System.Collections;

namespace Aya.DataBinding
{
    public abstract class DataBinderDictionary<TTarget, TDictionary> : DataBinder<TTarget, TDictionary> where TDictionary : IDictionary
    {
        public override bool CheckEquals(TDictionary data1, TDictionary data2)
        {
            if (data1 == null || data2 == null)
            {
                return false;
            }

            if (ReferenceEquals(data1, data2))
            {
                return true;
            }

            if (data1.Count != data2.Count)
            {
                return false;
            }

            foreach (var key in data1.Keys)
            {
                var v1 = data1[key];
                if (!data2.Contains(v1)) return false;
                var v2 = data2[key];
                if (!v1.Equals(v2)) return false;
            }

            return true;
        }
    }
}
