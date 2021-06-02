using System;

namespace Aya.DataBinding
{
    public class RuntimeValueBinder<T> : DataBinder<T>
    {
        public Func<T> Getter;
        public Action<T> Setter;

        public RuntimeValueBinder(string context, string key, Func<T> getter) : this(context, key, DataDirection.Source, getter, null)
        {
        }

        public RuntimeValueBinder(string context, string key, Action<T> setter) : this(context, key, DataDirection.Target, null, setter)
        {
        }

        public RuntimeValueBinder(string context, string key, DataDirection direction, Func<T> getter, Action<T> setter)
        {
            Context = context;
            Key = key;
            Direction = direction;
            Getter = getter;
            Setter = setter;
        }

        public override void SetData(T data)
        {
            Setter(data);
        }

        public override T GetData()
        {
            return Getter();
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
