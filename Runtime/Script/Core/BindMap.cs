using System;
using System.Collections.Generic;
using System.Reflection;

namespace Aya.DataBinding
{
    [Serializable]
    public class BindMap
    {
        public static Dictionary<Type, BindMap> MapDic = new Dictionary<Type, BindMap>();

        public Type Type;
        public Dictionary<PropertyInfo, BindAttribute> PropertyInfos = new Dictionary<PropertyInfo, BindAttribute>();
        public Dictionary<FieldInfo, BindAttribute> FieldInfos = new Dictionary<FieldInfo, BindAttribute>();

        #region Get BindMap
        
        public static BindMap GetBindMap(object target)
        {
            var type = target.GetType();
            var bindMap = GetBindMap(type);
            return bindMap;
        }

        public static BindMap GetBindMap(Type type)
        {
            if (!MapDic.TryGetValue(type, out var bindMap))
            {
                bindMap = new BindMap()
                {
                    Type = type
                };

                var bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
                var propertyInfos = type.GetProperties(bindingFlags);
                foreach (var propertyInfo in propertyInfos)
                {
                    var bindAttribute = propertyInfo.GetCustomAttribute<BindAttribute>();
                    if (bindAttribute == null) continue;
                    bindMap.PropertyInfos.Add(propertyInfo, bindAttribute);
                }

                var fieldInfos = type.GetFields(bindingFlags);
                foreach (var fieldInfo in fieldInfos)
                {
                    var bindAttribute = fieldInfo.GetCustomAttribute<BindAttribute>();
                    if (bindAttribute == null) continue;
                    bindMap.FieldInfos.Add(fieldInfo, bindAttribute);
                }
            }

            return bindMap;
        } 

        #endregion

        #region Bind / UnBind

        public static void Bind(object target)
        {
            var bindMap = GetBindMap(target);
            foreach (var kv in bindMap.PropertyInfos)
            {
                var propertyInfo = kv.Key;
                var bindAttribute = kv.Value;

                var context = bindAttribute.Context;
                var key = bindAttribute.Key;
                var direction = bindAttribute.Direction;

                if (bindAttribute is BindValueAttribute)
                {
                    var propertyBinder = new RuntimePropertyBinder(context, key, direction, target, propertyInfo, null);
                    propertyBinder.Bind();
                }

                if (bindAttribute is BindTypeAttribute)
                {
                    var obj = propertyInfo.GetValue(target, null);
                    var propertyBinder = new RuntimeTypeBinder(context, key, direction, obj);
                    propertyBinder.Bind();
                }
            }

            foreach (var kv in bindMap.FieldInfos)
            {
                var fieldInfo = kv.Key;
                var bindAttribute = kv.Value;

                var context = bindAttribute.Context;
                var key = bindAttribute.Key;
                var direction = bindAttribute.Direction;

                if (bindAttribute is BindValueAttribute)
                {
                    var propertyBinder = new RuntimePropertyBinder(context, key, direction, target, null, fieldInfo);
                    propertyBinder.Bind();
                }

                if (bindAttribute is BindTypeAttribute)
                {
                    var obj = fieldInfo.GetValue(target);
                    var propertyBinder = new RuntimeTypeBinder(context, key, direction, obj);
                    propertyBinder.Bind();
                }
            }
        }

        public static void UnBind(object target)
        {
            foreach (var binder in BindUpdater.Ins.UpdateSourceList)
            {
                if (!(binder is RuntimePropertyBinder propertyBinder)) continue;
                if (propertyBinder.Target != target) continue;
                BindUpdater.Ins.Remove(propertyBinder);
            }
        } 

        #endregion
    }
}