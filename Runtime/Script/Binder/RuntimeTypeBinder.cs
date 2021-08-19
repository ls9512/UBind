using System;
using System.Collections.Generic;

namespace Aya.DataBinding
{
    public class RuntimeTypeBinder : DataBinder<object, object>
    {
        public override Type TargetType => Target.GetType();

        private List<DataBinder> _binderCaches;

        public RuntimeTypeBinder(string context, string key, DataDirection direction, object target)
        {
            Context = context;
            Key = key;
            Direction = direction;
            Target = target;
        }

        public override void Bind()
        {
            if (_binderCaches == null)
            {
                _binderCaches = new List<DataBinder>();
                var type = TargetType;
                var (properties, fields) = TypeCaches.GetTypePropertiesAndFields(type);
                foreach (var propertyInfo in properties)
                {
                    var key = type.Name + "." + propertyInfo.Name + "." + Key;
                    var binder = new RuntimePropertyBinder(Context, key, Direction, Target, propertyInfo, null);
                    _binderCaches.Add(binder);
                }

                foreach (var fieldInfo in fields)
                {
                    var key = type.Name + "." + fieldInfo.Name + "." + Key;
                    var binder = new RuntimePropertyBinder(Context, key, Direction, Target, null, fieldInfo);
                    _binderCaches.Add(binder);
                }
            }

            foreach (var binder in _binderCaches)
            {
                binder.Bind();
                binder.UpdateTarget();
            }
        }

        public override void UnBind()
        {
            foreach (var binder in _binderCaches)
            {
                binder.UnBind();
            }
        }

        public override object Value { get; set; }
    }
}
