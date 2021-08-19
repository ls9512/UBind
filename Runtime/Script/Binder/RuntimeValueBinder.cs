using System;

namespace Aya.DataBinding
{
    public class RuntimeValueBinder<T> : DataBinder<T>
    {
        public override bool NeedUpdate => true;

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

        public override T Value
        {
            get => Getter();
            set => Setter(value);
        }
    }
}
