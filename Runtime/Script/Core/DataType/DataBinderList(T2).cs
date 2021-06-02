using System.Collections;

namespace Aya.DataBinding
{
    public abstract class DataBinderList<TTarget, TList> : DataBinder<TTarget, TList> where TList : IList
    {
        public override bool CheckEquals(TList data1, TList data2)
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

            foreach (var item in data1)
            {
                if (!data2.Contains(item)) return false;
            }

            return true;
        }
    }
}
