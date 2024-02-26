using System;

namespace Aya.DataBinding
{
    public class RuntimeValueBinder<T> : DataBinder<T>
    {
        public override bool NeedUpdate => true;

        public Func<T> Getter;
        public Action<T> Setter;

        public RuntimeValueBinder(string container, string key, Func<T> getter) : this(container, key, DataDirection.Source, getter, null)
        {
        }

        public RuntimeValueBinder(string container, string key, Action<T> setter) : this(container, key, DataDirection.Target, null, setter)
        {
        }

        public RuntimeValueBinder(string container, string key, DataDirection direction, Func<T> getter, Action<T> setter)
        {
            Container = container;
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
