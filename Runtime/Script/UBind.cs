using Aya.Sample;
using System;
using System.Reflection;

namespace Aya.DataBinding
{
    public static class UBind
    {
        #region Converter

        public static void RegisterConverter(Type sourceType, Type targetType, DataConverter dataConverter)
        {
            DataConverter.RegisterConverter(sourceType, targetType, dataConverter);
        }

        public static DataConverter GetConverter(Type sourceType, Type targetType)
        {
            return DataConverter.GetConverter(sourceType, targetType);
        }

        #endregion

        #region Bind Map

        public static void RegisterMap(object target)
        {
            BindMap.RegisterMap(target);
        }

        public static void DeRegisterMap(object target)
        {
            BindMap.DeRegisterMap(target);
        }

        #endregion

        #region Bind Value

        public static RuntimeValueBinder<T> BindSource<T>(string key, Func<T> getter)
        {
            return Bind(key, DataDirection.Source, getter, null);
        }
        public static RuntimeValueBinder<T> BindSource<T>(string container, string key, Func<T> getter)
        {
            return Bind(container, key, DataDirection.Source, getter, null);
        }

        public static RuntimeValueBinder<T> BindTarget<T>(string key, Action<T> setter)
        {
            return Bind(key, DataDirection.Target, null, setter);
        }

        public static RuntimeValueBinder<T> BindTarget<T>(string container, string key, Action<T> setter)
        {
            return Bind(container, key, DataDirection.Target, null, setter);
        }

        public static RuntimeValueBinder<T> BindBoth<T>(string key, Func<T> getter, Action<T> setter)
        {
            return Bind(key, DataDirection.Both, getter, setter);
        }
        public static RuntimeValueBinder<T> BindBoth<T>(string container, string key, Func<T> getter, Action<T> setter)
        {
            return Bind(container, key, DataDirection.Both, getter, setter);
        }

        public static RuntimeValueBinder<T> Bind<T>(string key, DataDirection direction, Func<T> getter, Action<T> setter)
        {
            return Bind(DataContainer.Default, key, direction, getter, setter); ;
        }

        public static RuntimeValueBinder<T> Bind<T>(string container, string key, DataDirection direction, Func<T> getter, Action<T> setter)
        {
            var dataBinder = new RuntimeValueBinder<T>(container, key, direction, getter, setter);
            dataBinder.Bind();
            return dataBinder;
        }

        #endregion

        #region Bind Value Pair

        public static (RuntimeValueBinder<T>, RuntimeValueBinder<T>) Bind<T>(string key, Func<T> sourceGetter, Action<T> targetSetter)
        {
            return Bind<T>(DataContainer.Default, key, sourceGetter, targetSetter);
        }

        public static (RuntimeValueBinder<T>, RuntimeValueBinder<T>)  Bind<T>(string container, string key, Func<T> sourceGetter, Action<T> targetSetter)
        {
            var sourceDataBinder = new RuntimeValueBinder<T>(container, key, DataDirection.Source, sourceGetter, null);
            var targetDataBinder = new RuntimeValueBinder<T>(container, key, DataDirection.Target, null, targetSetter);
            sourceDataBinder.Bind();
            targetDataBinder.Bind();
            return (sourceDataBinder, targetDataBinder);
        }

        #endregion

        #region Bind Type

        public static RuntimeTypeBinder BindSource(string key, object target)
        {
            return Bind(key, DataDirection.Source, target);
        }

        public static RuntimeTypeBinder BindSource(string container, string key, object target)
        {
            return Bind(container, key, DataDirection.Source, target);
        }

        public static RuntimeTypeBinder BindTarget(string key, object target)
        {
            return Bind(key, DataDirection.Target, target);
        }

        public static RuntimeTypeBinder BindTarget(string container, string key, object target)
        {
            return Bind(container, key, DataDirection.Target, target);
        }

        public static RuntimeTypeBinder Bind(string key, DataDirection direction, object target)
        {
            return Bind(DataContainer.Default, key, direction, target);
        }

        public static RuntimeTypeBinder Bind(string container, string key, DataDirection direction, object target)
        {
            var dataBinder = new RuntimeTypeBinder(container, key, direction, target);
            dataBinder.Bind();
            return dataBinder;
        }

        #endregion

        #region Bind Property By Name

        public static RuntimePropertyBinder BindSource(string key, object target, string propertyName)
        {
            return Bind(key, DataDirection.Source, target, propertyName);
        }

        public static RuntimePropertyBinder BindSource(string container, string key, object target, string propertyName)
        {
            return Bind(container, key, DataDirection.Source, target, propertyName);
        }

        public static RuntimePropertyBinder BindTarget(string key, object target, string propertyName)
        {
            return Bind(key, DataDirection.Target, target, propertyName);
        }

        public static RuntimePropertyBinder BindTarget(string container, string key, object target, string propertyName)
        {
            return Bind(container, key, DataDirection.Target, target, propertyName);
        }

        public static RuntimePropertyBinder BindBoth(string key, object target, string propertyName)
        {
            return Bind(key, DataDirection.Both, target, propertyName);
        }

        public static RuntimePropertyBinder BindBoth(string container, string key, object target, string propertyName)
        {
            return Bind(container, key, DataDirection.Both, target, propertyName);
        }

        public static RuntimePropertyBinder Bind(string key, DataDirection direction, object target, string propertyName)
        {
            return Bind(DataContainer.Default, key, direction, target, propertyName);
        }

        public static RuntimePropertyBinder Bind(string container, string key, DataDirection direction, object target, string propertyName)
        {
            var type = target.GetType();
            var (propertyInfo, fieldInfo) = TypeCaches.GetTypePropertyOrFieldByName(type, propertyName);
            return Bind(container, key, direction, target, propertyInfo, fieldInfo);
        }

        #endregion

        #region Bind Property By Info

        public static RuntimePropertyBinder BindSource(string key, object target, PropertyInfo propertyInfo)
        {
            return Bind(key, DataDirection.Source, target, propertyInfo, null);
        }

        public static RuntimePropertyBinder BindSource(string container, string key, object target, PropertyInfo propertyInfo)
        {
            return Bind(container, key, DataDirection.Source, target, propertyInfo, null);
        }

        public static RuntimePropertyBinder BindSource(string key, object target, FieldInfo fieldInfo)
        {
            return Bind(key, DataDirection.Source, target, null, fieldInfo);
        }

        public static RuntimePropertyBinder BindSource(string container, string key, object target, FieldInfo fieldInfo)
        {
            return Bind(container, key, DataDirection.Source, target, null, fieldInfo);
        }

        public static RuntimePropertyBinder BindTarget(string key, object target, PropertyInfo propertyInfo)
        {
            return Bind(key, DataDirection.Target, target, propertyInfo, null);
        }

        public static RuntimePropertyBinder BindTarget(string container, string key, object target, PropertyInfo propertyInfo)
        {
            return Bind(container, key, DataDirection.Target, target, propertyInfo, null);
        }

        public static RuntimePropertyBinder BindTarget(string key, object target, FieldInfo fieldInfo)
        {
            return Bind(key, DataDirection.Target, target, null, fieldInfo);
        }

        public static RuntimePropertyBinder BindTarget(string container, string key, object target, FieldInfo fieldInfo)
        {
            return Bind(container, key, DataDirection.Target, target, null, fieldInfo);
        }

        public static RuntimePropertyBinder BindBoth(string key, object target, PropertyInfo propertyInfo)
        {
            return Bind(key, DataDirection.Both, target, propertyInfo, null);
        }

        public static RuntimePropertyBinder BindBoth(string container, string key, object target, PropertyInfo propertyInfo)
        {
            return Bind(container, key, DataDirection.Both, target, propertyInfo, null);
        }

        public static RuntimePropertyBinder BindBoth(string key, object target, FieldInfo fieldInfo)
        {
            return Bind(key, DataDirection.Both, target, null, fieldInfo);
        }

        public static RuntimePropertyBinder BindBoth(string container, string key, object target, FieldInfo fieldInfo)
        {
            return Bind(container, key, DataDirection.Both, target, null, fieldInfo);
        }

        public static RuntimePropertyBinder Bind(string key, DataDirection direction, object target, PropertyInfo propertyInfo, FieldInfo fieldInfo)
        {
            return Bind(DataContainer.Default, key, direction, target, propertyInfo, fieldInfo);
        }

        public static RuntimePropertyBinder Bind(string container, string key, DataDirection direction, object target, PropertyInfo propertyInfo, FieldInfo fieldInfo)
        {
            var dataBinder = new RuntimePropertyBinder(container, key, direction, target, propertyInfo, fieldInfo);
            dataBinder.Bind();
            return dataBinder;
        } 

        #endregion
    }
}
