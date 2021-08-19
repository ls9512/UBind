using System;
using System.Globalization;
using System.Reflection;
using UnityEngine;

namespace Aya.DataBinding
{
    [AddComponentMenu("Data Binding/Property Binder")]
    public class PropertyBinder : ComponentBinder<Component, object, RuntimePropertyBinder<Component>>
    {
        public override bool NeedUpdate => true;

        public string Property;

        public override RuntimePropertyBinder<Component> CreateDataBinder()
        {
            var dataBinder = new RuntimePropertyBinder<Component>
            {
                Target = Target,
                TargetType = Target.GetType(),
                Property = Property,
                Context = Context,
                Direction = Direction,
                Key = Key
            };

            return dataBinder;
        }

        public override void Reset()
        {
            Target = null;
        }
    }

    public class RuntimePropertyBinder<TTarget> : DataBinder<TTarget, object>
    {
        public string Property;

        public override bool NeedUpdate => true;

        public override object Value
        {
            get
            {
                if (FiledInfo != null)
                {
                    var data = FiledInfo.GetValue(Target);
                    return data;
                }

                if (PropertyInfo != null)
                {
                    var data = PropertyInfo.GetValue(Target, null);
                    return data;
                }

                return default;
            }
            set
            {
                var data = value;
                var dataType = data != null ? data.GetType() : typeof(object);
                if (FiledInfo != null)
                {
                    if (data != null && dataType != FiledInfo.FieldType)
                    {
                        var convertData = Convert.ChangeType(data, FiledInfo.FieldType, CultureInfo.InvariantCulture);
                        FiledInfo.SetValue(Target, convertData);
                    }
                    else
                    {
                        FiledInfo.SetValue(Target, data);
                    }
                }

                if (PropertyInfo != null)
                {
                    if (data != null && dataType != PropertyInfo.PropertyType)
                    {
                        var convertData = Convert.ChangeType(data, PropertyInfo.PropertyType, CultureInfo.InvariantCulture);
                        PropertyInfo.SetValue(Target, convertData, null);
                    }
                    else
                    {
                        PropertyInfo.SetValue(Target, data, null);
                    }
                }
            }
        }

        public FieldInfo FiledInfo
        {
            get
            {
                if (_filedInfo == null || _filedInfo.Name != Property)
                {
                    _filedInfo = TargetType.GetField(Property, TypeCaches.DefaultBindingFlags);
                    DataType = _filedInfo?.FieldType;
                }

                return _filedInfo;
            }

            internal set => _filedInfo = value;
        }

        private FieldInfo _filedInfo;

        public PropertyInfo PropertyInfo
        {
            get
            {
                if (_propertyInfo == null || _propertyInfo.Name != Property)
                {
                    _propertyInfo = TargetType.GetProperty(Property, TypeCaches.DefaultBindingFlags);
                    DataType = _propertyInfo?.PropertyType;
                }

                return _propertyInfo;
            }

            internal set => _propertyInfo = value;
        }

        private PropertyInfo _propertyInfo;
    }
}
