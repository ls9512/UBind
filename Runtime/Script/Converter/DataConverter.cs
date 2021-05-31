using System;
using System.Collections.Generic;

namespace Aya.DataBinding
{
    public abstract class DataConverter
    {
        public static DataConverter Default = new CommonConverter();
        internal static Dictionary<(Type, Type), DataConverter> ConverterDic = new Dictionary<(Type, Type), DataConverter>();

        public abstract object To(object value, Type convertType);

        public static void Register(Type sourceType, Type targetType, DataConverter dataConverter)
        {
            ConverterDic[(sourceType, targetType)] = dataConverter;
        }

        public static DataConverter GetConverter(Type sourceType, Type targetType)
        {
            if (ConverterDic.TryGetValue((sourceType, targetType), out var converter))
            {
                return converter;
            }

            return Default;
        }
    }
}
