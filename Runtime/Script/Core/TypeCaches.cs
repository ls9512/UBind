using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace Aya.DataBinding
{
    internal static class TypeCaches
    {
        public static Assembly[] Assemblies
        {
            get
            {
                if (_assemblies == null)
                {
                    _assemblies = AppDomain.CurrentDomain.GetAssemblies();
                }

                return _assemblies;
            }
        }

        private static Assembly[] _assemblies;

        public static Dictionary<Assembly, List<Type>> AssemblyTypeDic = new Dictionary<Assembly, List<Type>>();
        public static Dictionary<Type, List<PropertyInfo>> TypePropertyDic = new Dictionary<Type, List<PropertyInfo>>();
        public static Dictionary<Type, Dictionary<string, PropertyInfo>> TypeNamePropertyDic = new Dictionary<Type, Dictionary<string, PropertyInfo>>();
        public static Dictionary<Type, List<FieldInfo>> TypeFieldDic = new Dictionary<Type, List<FieldInfo>>();
        public static Dictionary<Type, Dictionary<string, FieldInfo>> TypeNameFiledDic = new Dictionary<Type, Dictionary<string, FieldInfo>>();

        public static List<Type> BaseTypes = new List<Type>()
        {
            typeof(ushort),
            typeof(short),
            typeof(uint),
            typeof(int),
            typeof(ulong),
            typeof(long),
            typeof(float),
            typeof(double),
            typeof(decimal),
            typeof(char),
            typeof(string),
            typeof(bool),
            typeof(byte),
            typeof(Enum),
            typeof(object)
        };

        public static List<Type> BindableTypes = new List<Type>(BaseTypes)
        {
            typeof(Vector2),
            typeof(Vector2Int),
            typeof(Vector3),
            typeof(Vector3Int),
            typeof(Vector4),
            typeof(Quaternion),
            typeof(Color),
            typeof(Rect),
            typeof(Matrix4x4),
            typeof(UnityEngine.Object)
        };

        public static Dictionary<Type, string> AutoBindComponentTypeDic = new Dictionary<Type, string>()
        {
            {typeof(Text), "text"},
            {typeof(InputField), "text"},
            {typeof(Dropdown), "value"},
            {typeof(Slider), "value"},
            {typeof(Scrollbar), "value"},
            {typeof(Toggle), "isOn"},
            {typeof(CanvasGroup), "alpha"},
            {typeof(Image), "sprite"},
            {typeof(RawImage), "texture"},
        };

        public static BindingFlags DefaultBindingFlags { get; set; } = BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        public static Assembly GetAssemblyByName(string assemblyName)
        {
            if (string.IsNullOrEmpty(assemblyName)) return null;
            foreach (var assembly in Assemblies)
            {
                var temp = assembly.GetName().Name;
                if (temp == assemblyName) return assembly;
            }

            return null;
        }

        public static Type GetTypeByName(string assemblyName, string typeName)
        {
            var assembly = GetAssemblyByName(assemblyName);
            if (assembly == null) return null;
            if (string.IsNullOrEmpty(typeName)) return null;
            var type = assembly.GetType(typeName);
            return type;
        }

        public static List<Type> GetAssemblyTypes(Assembly assembly)
        {
            if (!AssemblyTypeDic.TryGetValue(assembly, out var result))
            {
                result = new List<Type>();
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    result.Add(type);
                }

                AssemblyTypeDic.Add(assembly, result);
            }

            return result;
        }

        public static List<PropertyInfo> GetTypeProperties(Type type)
        {
            if (!TypePropertyDic.TryGetValue(type, out var result))
            {
                result = new List<PropertyInfo>();
                var properties = type.GetProperties(DefaultBindingFlags);
                result.AddRange(properties);
                TypePropertyDic.Add(type, result);
            }

            return result;
        }

        public static PropertyInfo GetTypePropertyByName(Type type, string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName)) return null;
            if (!TypeNamePropertyDic.TryGetValue(type, out var propertyDic))
            {
                propertyDic = new Dictionary<string, PropertyInfo>();
                TypeNamePropertyDic.Add(type, propertyDic);
            }

            if (!propertyDic.TryGetValue(propertyName, out var propertyInfo))
            {
                propertyInfo = type.GetProperty(propertyName, DefaultBindingFlags);
                if (propertyInfo != null)
                {
                    propertyDic.Add(propertyName, propertyInfo);
                }
            }

            return propertyInfo;
        }

        public static List<FieldInfo> GetTypeFields(Type type)
        {
            if (!TypeFieldDic.TryGetValue(type, out var result))
            {
                result = new List<FieldInfo>();
                var fields = type.GetFields(DefaultBindingFlags);
                result.AddRange(fields);
                TypeFieldDic.Add(type, result);
            }

            return result;
        }

        public static FieldInfo GetTypeFieldByName(Type type, string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName)) return null;
            if (!TypeNameFiledDic.TryGetValue(type, out var fieldDic))
            {
                fieldDic = new Dictionary<string, FieldInfo>();
                TypeNameFiledDic.Add(type, fieldDic);
            }

            if (!fieldDic.TryGetValue(fieldName, out var fieldInfo))
            {
                fieldInfo = type.GetField(fieldName, DefaultBindingFlags);
                if (fieldInfo != null)
                {
                    fieldDic.Add(fieldName, fieldInfo);
                }
            }

            return fieldInfo;
        }

        public static (List<PropertyInfo>, List<FieldInfo>) GetTypePropertiesAndFields(Type type)
        {
            var property = GetTypeProperties(type);
            var filed = GetTypeFields(type);
            return (property, filed);
        }

        public static (PropertyInfo, FieldInfo) GetTypePropertyOrFieldByName(Type type, string name)
        {
            var property = GetTypePropertyByName(type, name);
            var filed = GetTypeFieldByName(type, name);
            return (property, filed);
        }

        public static bool CheckTypeHasPropertyOrFieldByName(Type type, string name)
        {
            var (property, filed) = GetTypePropertyOrFieldByName(type, name);
            return property != null || filed != null;
        }
    }
}
