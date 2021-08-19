using System;
using System.Reflection;

namespace Aya.DataBinding
{
    public enum DataDirection
    {
        Source = 0,
        Target = 1,
        Both = 2,
    }

    public enum UpdateType
    {
        None = -1,
        Update = 0,
        LateUpdate = 1,
        FixedUpdate = 2,
    }

    public abstract class DataBinder
    {
        public string Context = DataContext.Default;
        public string Key;
        public DataDirection Direction = DataDirection.Target;

        // Only work when NeedUpdate is true
        public UpdateType UpdateType = UpdateType.Update;

        #region Reflection Cache

        public virtual Type DataType { get; internal set; }

        public virtual Type BinderType { get; internal set; }

        public virtual PropertyInfo ValuePropertyInfo
        {
            get
            {
                if (_valuePropertyInfo == null)
                {
                    _valuePropertyInfo = BinderType.GetProperty("Value");
                }

                return _valuePropertyInfo;
            }
        }

        private PropertyInfo _valuePropertyInfo;

        #endregion

        public bool IsDestination => Direction == DataDirection.Target || Direction == DataDirection.Both;
        public bool IsSource => Direction == DataDirection.Source || Direction == DataDirection.Both;

        public DataContext DataContext { get; internal set; }

        public virtual bool NeedUpdate => false;

        public virtual void UpdateSource()
        {
        }

        public virtual void UpdateTarget()
        {
        }

        public virtual void AddListener()
        {
        }

        public virtual void Bind()
        {
            if (NeedUpdate)
            {
                BindUpdater.Ins.Add(this);
            }
            else
            {
                AddListener();
            }

            DataContext.Bind(this);
        }

        public virtual void RemoveListener()
        {
        }


        public virtual void UnBind()
        {
            if (NeedUpdate)
            {
                BindUpdater.Ins.Remove(this);
            }
            else
            {
                RemoveListener();
            }

            DataContext.UnBind(this);
        }

        public abstract void Broadcast();

        internal virtual void SetDataInternal(object data)
        {
            if (data == null)
            {
                ValuePropertyInfo.SetValue(this, null);
                return;
            }

            var converter = DataConverter.GetConverter(data.GetType(), DataType);
            var convertData = converter.To(data, DataType);
            ValuePropertyInfo.SetValue(this, convertData);
        }

        internal virtual object GetDataInternal(Type convertType)
        {
            var data = ValuePropertyInfo.GetValue(this);
            if (data == null) return default;

            var converter = DataConverter.GetConverter(DataType, convertType);
            var convertData = converter.To(data, convertType);
            return convertData;
        }
    }
}
