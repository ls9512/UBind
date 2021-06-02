using UnityEngine;

namespace Aya.DataBinding
{
    public abstract class ComponentBinder<TComponent, TValue, TDataBinder> : MonoBehaviour
        where TDataBinder : DataBinder<TComponent, TValue>, new()
        where TComponent : Component
    {
        public string Context = DataContext.Default;
        public string Key;
        public DataDirection Direction = DataDirection.Target;
        public UpdateType UpdateType = UpdateType.Update;
        public TComponent Target;

        public TDataBinder DataBinder { get; internal set; }
        public DataContext DataContext => DataBinder?.DataContext;

        public bool IsDestination => Direction == DataDirection.Target || Direction == DataDirection.Both;
        public bool IsSource => Direction == DataDirection.Source || Direction == DataDirection.Both;

        public virtual bool NeedUpdate { get; set; } = false;

        public virtual void Awake()
        {
        }

        public virtual void Start()
        {

        }

        public virtual void OnEnable()
        {
            if (DataBinder == null)
            {
                DataBinder = CreateDataBinder();
            }
            
            DataBinder.Bind();
            DataBinder.UpdateSource();
            DataBinder.UpdateTarget();
        }

        public virtual TDataBinder CreateDataBinder()
        {
            var dataBinder = new TDataBinder
            {
                Target = Target,
                TargetType = typeof(TComponent),
                Context = Context,
                Direction = Direction,
                UpdateType = UpdateType,
                Key = Key
            };

            return dataBinder;
        }

        public virtual void Update()
        {
        }

        public virtual void LateUpdate()
        {
        }

        public virtual void FixedUpdate()
        {
        }

        public virtual void OnDisable()
        {
            DataBinder.UnBind();
        }

        public virtual void OnValidate()
        {

        }

        public virtual void Reset()
        {
            Target = GetComponentInChildren<TComponent>();
        }
    }
}