
namespace Aya.DataBinding
{
    public abstract class DataBinderArray<TTarget, TData> : DataBinder<TTarget, TData[]>
    {
        public override bool CheckEquals(TData[] data1, TData[] data2)
        {
            if (data1 == null || data2 == null)
            {
                return false;
            }

            if (ReferenceEquals(data1, data2))
            {
                return true;
            }

            if (data1.Length != data2.Length)
            {
                return false;
            }

            foreach (var item in data1)
            {
                if (!ContainsInArray(data2, item)) return false;
            }

            return true;
        }

        public virtual bool ContainsInArray(TData[] array, TData data)
        {
            foreach (var item in array)
            {
                if (item.Equals(data)) return true;
            }

            return false;
        }
    }
}
