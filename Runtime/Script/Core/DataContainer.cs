using System.Collections.Generic;

namespace Aya.DataBinding
{
    public class DataContainer
    {
        public const string Default = "Default";

        public string Key { get; protected set; }

        #region Data Context
       
        public static Dictionary<string, DataContainer> ContextDic = new Dictionary<string, DataContainer>();

        public static DataContainer GetContainer(string key)
        {
            if (ContextDic.TryGetValue(key, out var dataContainer)) return dataContainer;
            dataContainer = new DataContainer()
            {
                Key = key
            };

            ContextDic.Add(key, dataContainer);

            return dataContainer;
        }

        #endregion

        #region Data Binder

        public Dictionary<string, List<DataBinder>> DestinationDic = new Dictionary<string, List<DataBinder>>();
        public Dictionary<string, List<DataBinder>> SourceDic = new Dictionary<string, List<DataBinder>>();

        public List<DataBinder> GetDestinations(string key)
        {
            if (DestinationDic.TryGetValue(key, out var dataBinders)) return dataBinders;
            dataBinders = new List<DataBinder>();
            DestinationDic.Add(key, dataBinders);

            return dataBinders;
        }

        public List<DataBinder> GetSources(string key)
        {
            if (SourceDic.TryGetValue(key, out var dataBinders)) return dataBinders;
            dataBinders = new List<DataBinder>();
            SourceDic.Add(key, dataBinders);

            return dataBinders;
        }

        #endregion

        #region Bind / UnBind

        public static void Bind(DataBinder dataBinder)
        {
            var dataContext = GetContainer(dataBinder.Container);
            if (dataBinder.IsDestination) BindDestination(dataContext, dataBinder);
            if (dataBinder.IsSource) BindSource(dataContext, dataBinder);
        }

        internal static void BindDestination(DataContainer dataContainer, DataBinder dataBinder)
        {
            var dataBinders = dataContainer.GetDestinations(dataBinder.Key);
            if (dataBinders.Contains(dataBinder)) return;
            dataBinder.DataContainer = dataContainer;
            dataBinders.Add(dataBinder);
        }

        internal static void BindSource(DataContainer dataContainer, DataBinder dataBinder)
        {
            var dataBinders = dataContainer.GetSources(dataBinder.Key);
            if (dataBinders.Contains(dataBinder)) return;
            dataBinder.DataContainer = dataContainer;
            dataBinders.Add(dataBinder);
        }

        public static void UnBind(DataBinder dataBinder)
        {
            var dataContext = GetContainer(dataBinder.Container);
            if (dataBinder.IsDestination) UnBindDestination(dataContext, dataBinder);
            if (dataBinder.IsSource) UnBindSource(dataContext, dataBinder);
        }

        internal static void UnBindDestination(DataContainer dataContainer, DataBinder dataBinder)
        {
            var dataBinders = dataContainer.GetDestinations(dataBinder.Key);
            if (!dataBinders.Contains(dataBinder)) return;
            dataBinder.DataContainer = null;
            dataBinders.Remove(dataBinder);
        }

        internal static void UnBindSource(DataContainer dataContainer, DataBinder dataBinder)
        {
            var dataBinders = dataContainer.GetSources(dataBinder.Key);
            if (!dataBinders.Contains(dataBinder)) return;
            dataBinder.DataContainer = null;
            dataBinders.Remove(dataBinder);
        }

        #endregion

        #region Data

        public static T GetData<T>(string containerKey, string key)
        {
            var dataContext = GetContainer(containerKey);
            var result = dataContext.GetData<T>(key);
            return result;
        }

        public T GetData<T>(string key)
        {
            var dataBinders = GetSources(key);
            if (dataBinders.Count == 0) return default;
            for (var i = 0; i < dataBinders.Count; i++)
            {
                var dataBinder = dataBinders[i];
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
