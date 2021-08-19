using System.Collections.Generic;

namespace Aya.DataBinding
{
    public class DataContext
    {
        public const string Default = "Default";

        public string Key { get; protected set; }

        #region Data Context
       
        public static Dictionary<string, DataContext> ContextDic = new Dictionary<string, DataContext>();

        public static DataContext GetContext(string key)
        {
            if (!ContextDic.TryGetValue(key, out var dataContext))
            {
                dataContext = new DataContext()
                {
                    Key = key
                };

                ContextDic.Add(key, dataContext);
            }

            return dataContext;
        }

        #endregion

        #region Data Binder

        public Dictionary<string, List<DataBinder>> DestinationDic = new Dictionary<string, List<DataBinder>>();
        public Dictionary<string, List<DataBinder>> SourceDic = new Dictionary<string, List<DataBinder>>();

        public List<DataBinder> GetDestinations(string key)
        {
            if (!DestinationDic.TryGetValue(key, out var dataBinders))
            {
                dataBinders = new List<DataBinder>();
                DestinationDic.Add(key, dataBinders);
            }

            return dataBinders;
        }

        public List<DataBinder> GetSources(string key)
        {
            if (!SourceDic.TryGetValue(key, out var dataBinders))
            {
                dataBinders = new List<DataBinder>();
                SourceDic.Add(key, dataBinders);
            }

            return dataBinders;
        }

        #endregion

        #region Bind / UnBind

        public static void Bind(DataBinder dataBinder)
        {
            var dataContext = GetContext(dataBinder.Context);
            if (dataBinder.IsDestination) BindDestination(dataContext, dataBinder);
            if (dataBinder.IsSource) BindSource(dataContext, dataBinder);
        }

        internal static void BindDestination(DataContext dataContext, DataBinder dataBinder)
        {
            var dataBinders = dataContext.GetDestinations(dataBinder.Key);
            if (!dataBinders.Contains(dataBinder))
            {
                dataBinder.DataContext = dataContext;
                dataBinders.Add(dataBinder);
            }
        }

        internal static void BindSource(DataContext dataContext, DataBinder dataBinder)
        {
            var dataBinders = dataContext.GetSources(dataBinder.Key);
            if (!dataBinders.Contains(dataBinder))
            {
                dataBinder.DataContext = dataContext;
                dataBinders.Add(dataBinder);
            }
        }

        public static void UnBind(DataBinder dataBinder)
        {
            var dataContext = GetContext(dataBinder.Context);
            if (dataBinder.IsDestination) UnBindDestination(dataContext, dataBinder);
            if (dataBinder.IsSource) UnBindSource(dataContext, dataBinder);
        }

        internal static void UnBindDestination(DataContext dataContext, DataBinder dataBinder)
        {
            var dataBinders = dataContext.GetDestinations(dataBinder.Key);
            if (dataBinders.Contains(dataBinder))
            {
                dataBinder.DataContext = null;
                dataBinders.Remove(dataBinder);
            }
        }

        internal static void UnBindSource(DataContext dataContext, DataBinder dataBinder)
        {
            var dataBinders = dataContext.GetSources(dataBinder.Key);
            if (dataBinders.Contains(dataBinder))
            {
                dataBinder.DataContext = null;
                dataBinders.Remove(dataBinder);
            }
        }

        #endregion

        #region Data

        public static T GetData<T>(string contextKey, string key)
        {
            var dataContext = GetContext(contextKey);
            var result = dataContext.GetData<T>(key);
            return result;
        }

        public T GetData<T>(string key)
        {
            var dataBinders = GetSources(key);
            if (dataBinders.Count == 0) return default;
            foreach (var dataBinder in dataBinders)
            {
                var sourceDataBinder = dataBinder as DataBinder<T>;
                if (sourceDataBinder == null) continue;
                var result = sourceDataBinder.Value;
                return result;
            }

            var tryConvertData = (T)dataBinders[0].GetDataInternal(typeof(T));
            return tryConvertData;
        }

        #endregion
    }
}
