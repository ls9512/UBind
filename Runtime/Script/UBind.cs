using System;
using System.Reflection;

namespace Aya.DataBinding
{
    public static class UBind
    {
        #region Bind Value
        
        public static RuntimeValueBinder<T> BindSource<T>(string key, Func<T> getter)
        {
            return Bind(key, DataDirection.Source, getter, null);
        }
        public static RuntimeValueBinder<T> BindSource<T>(string context, string key, Func<T> getter)
        {
            return Bind(context, key, DataDirection.Source, getter, null);
        }

        public static RuntimeValueBinder<T> BindTarget<T>(string key, Action<T> setter)
        {
            return Bind(key, DataDirection.Target, null, setter);
        }

        public static RuntimeValueBinder<T> BindTarget<T>(string context, string key, Action<T> setter)
        {
            return Bind(context, key, DataDirection.Target, null, setter);
        }

        public static RuntimeValueBinder<T> BindBoth<T>(string key, Func<T> getter, Action<T> setter)
        {
            return Bind(key, DataDirection.Both, getter, setter);
        }
        public static RuntimeValueBinder<T> BindBoth<T>(string context, string key, Func<T> getter, Action<T> setter)
        {
            return Bind(context, key, DataDirection.Both, getter, setter);
        }

        public static RuntimeValueBinder<T> Bind<T>(string key, DataDirection direction, Func<T> getter, Action<T> setter)
        {
            return Bind(DataContext.Default, key, direction, getter, setter); ;
        }

        public static RuntimeValueBinder<T> Bind<T>(string context, string key, DataDirection direction, Func<T> getter, Action<T> setter)
        {
            var dataBinder = new RuntimeValueBinder<T>(context, key, direction, getter, setter);
            dataBinder.Bind();
            return dataBinder;
        }

        #endregion

        #region Bind Value Pair

        public static (RuntimeValueBinder<T>, RuntimeValueBinder<T>) Bind<T>(string key, Func<T> sourceGetter, Action<T> targetSetter)
        {
            return Bind<T>(DataContext.Default, key, sourceGetter, targetSetter);
        }

        public static (RuntimeValueBinder<T>, RuntimeValueBinder<T>)  Bind<T>(string context, string key, Func<T> sourceGetter, Action<T> targetSetter)
        {
            var sourceDataBinder = new RuntimeValueBinder<T>(context, key, DataDirection.Source, sourceGetter, null);
            var targetDataBinder = new RuntimeValueBinder<T>(context, key, DataDirection.Target, null, targetSetter);
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

        public static RuntimeTypeBinder BindSource(string context, string key, object target)
        {
            return Bind(context, key, DataDirection.Source, target);
        }

        public static RuntimeTypeBinder BindTarget(string key, object target)
        {
            return Bind(key, DataDirection.Target, target);
        }

        public static RuntimeTypeBinder BindTarget(string context, string key, object target)
        {
            return Bind(context, key, DataDirection.Target, target);
        }

        public static RuntimeTypeBinder Bind(string key, DataDirection direction, object target)
        {
            return Bind(DataContext.Default, key, direction, target);
        }

        public static RuntimeTypeBinder Bind(string context, string key, DataDirection direction, object target)
        {
            var dataBinder = new RuntimeTypeBinder(context, key, direction, target);
            dataBinder.Bind();
            return dataBinder;
        }

        #endregion

        #region Bind Property By Name

        public static RuntimePropertyBinder BindSource(string key, object target, string propertyName)
        {
            return Bind(key, DataDirection.Source, target, propertyName);
        }

        public static RuntimePropertyBinder BindSource(string context, string key, object target, string propertyName)
        {
            return Bind(context, key, DataDirection.Source, target, propertyName);
        }

        public static RuntimePropertyBinder BindTarget(string key, object target, string propertyName)
        {
            return Bind(key, DataDirection.Target, target, propertyName);
        }

        public static RuntimePropertyBinder BindTarget(string context, string key, object target, string propertyName)
        {
            return Bind(context, key, DataDirection.Target, target, propertyName);
        }

        public static RuntimePropertyBinder BindBoth(string key, object target, string propertyName)
        {
            return Bind(key, DataDirection.Both, target, propertyName);
        }

        public static RuntimePropertyBinder BindBoth(string context, string key, object target, string propertyName)
        {
            return Bind(context, key, DataDirection.Both, target, propertyName);
        }

        public static RuntimePropertyBinder Bind(string key, DataDirection direction, object target, string propertyName)
        {
            return Bind(DataContext.Default, key, direction, target, propertyName);
        }

        public static RuntimePropertyBinder Bind(string context, string key, DataDirection direction, object target, string propertyName)
        {
            var (propertyInfo, fieldInfo) = TypeCaches.GetTypePropertyOrFieldByName(target.GetType(), propertyName);
            return Bind(context, key, direction, target, propertyInfo, fieldInfo);
        }

        #endregion

        #region Bind Property By Info

        public static RuntimePropertyBinder BindSource(string key, object target, PropertyInfo propertyInfo)
        {
            return Bind(key, DataDirection.Source, target, propertyInfo, null);
        }

        public static RuntimePropertyBinder BindSource(string context, string key, object target, PropertyInfo propertyInfo)
        {
            return Bind(context, key, DataDirection.Source, target, propertyInfo, null);
        }

        public static RuntimePropertyBinder BindSource(string key, object target, FieldInfo fieldInfo)
        {
            return Bind(key, DataDirection.Source, target, null, fieldInfo);
        }

        public static RuntimePropertyBinder BindSource(string context, string key, object target, FieldInfo fieldInfo)
        {
            return Bind(context, key, DataDirection.Source, target, null, fieldInfo);
        }

        public static RuntimePropertyBinder BindTarget(string key, object target, PropertyInfo propertyInfo)
        {
            return Bind(key, DataDirection.Target, target, propertyInfo, null);
        }

        public static RuntimePropertyBinder BindTarget(string context, string key, object target, PropertyInfo propertyInfo)
        {
            return Bind(context, key, DataDirection.Target, target, propertyInfo, null);
        }

        public static RuntimePropertyBinder BindTarget(string key, object target, FieldInfo fieldInfo)
        {
            return Bind(key, DataDirection.Target, target, null, fieldInfo);
        }

        public static RuntimePropertyBinder BindTarget(string context, string key, object target, FieldInfo fieldInfo)
        {
            return Bind(context, key, DataDirection.Target, target, null, fieldInfo);
        }

        public static RuntimePropertyBinder BindBoth(string key, object target, PropertyInfo propertyInfo)
        {
            return Bind(key, DataDirection.Both, target, propertyInfo, null);
        }

        public static RuntimePropertyBinder BindBoth(string context, string key, object target, PropertyInfo propertyInfo)
        {
            return Bind(context, key, DataDirection.Both, target, propertyInfo, null);
        }

        public static RuntimePropertyBinder BindBoth(string key, object target, FieldInfo fieldInfo)
        {
            return Bind(key, DataDirection.Both, target, null, fieldInfo);
        }

        public static RuntimePropertyBinder BindBoth(string context, string key, object target, FieldInfo fieldInfo)
        {
            return Bind(context, key, DataDirection.Both, target, null, fieldInfo);
        }

        public static RuntimePropertyBinder Bind(string key, DataDirection direction, object target, PropertyInfo propertyInfo, FieldInfo fieldInfo)
        {
            return Bind(DataContext.Default, key, direction, target, propertyInfo, fieldInfo);
        }

        public static RuntimePropertyBinder Bind(string context, string key, DataDirection direction, object target, PropertyInfo propertyInfo, FieldInfo fieldInfo)
        {
            var dataBinder = new RuntimePropertyBinder(context, key, direction, target, propertyInfo, fieldInfo);
            dataBinder.Bind();
            return dataBinder;
        } 

        #endregion
    }
}
