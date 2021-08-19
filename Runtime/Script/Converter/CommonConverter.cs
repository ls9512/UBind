using System;
using System.Globalization;

namespace Aya.DataBinding
{
    public class CommonConverter : DataConverter
    {
        public override object To(object data, Type convertType)
        {
            if (data == null) return null;
            var dataType = data.GetType();
            if (dataType == convertType || dataType == typeof(object))
            {
                return data;
            }
            else if (convertType == typeof(string))
            {
                var convertData = data.ToString();
                return convertData;
            }
            else
            {
                try
                {
                    var convertData = Convert.ChangeType(data, convertType, CultureInfo.InvariantCulture);
                    return convertData;
                }
                catch
                {
                    var result =  convertType.IsValueType ? Activator.CreateInstance(convertType) : null;
                    return result;
                }
            }
        }
    }
}
