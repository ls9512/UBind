using System;
using System.Reflection;

namespace Aya.DataBinding
{
    public class RuntimePropertyBinder : RuntimePropertyBinder<object>
    {
        public override Type TargetType => Target.GetType();

        public RuntimePropertyBinder(string context, string key, DataDirection direction, object target, PropertyInfo propertyInfo, FieldInfo fieldInfo)
        {
            Context = context;
            Key = key;
            Direction = direction;
            Target = target;
            PropertyInfo = propertyInfo;
            FiledInfo = fieldInfo;

            if (propertyInfo != null)
            {
                Property = propertyInfo.Name;
            }

            if (fieldInfo != null)
            {
                Property = fieldInfo.Name;
            }
        }

        public override void Bind()
        {
            base.Bind();
            BindUpdater.Ins.Add(this);
        }

        public override void UnBind()
        {
            base.UnBind();
            BindUpdater.Ins.Remove(this);
        }
    }
}