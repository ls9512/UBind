using System;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.DataBinding
{
    [Serializable]
    public class TypeBindMap
    {
        public string Property;
        public Component Target;
        public string TargetProperty;
    }

    [AddComponentMenu("Data Binding/Type Binder")]
    public class TypeBinder : MonoBehaviour
    {
        public string Context = DataContext.Default;
        public string Key;
        public DataDirection Direction = DataDirection.Target;

        public string Assembly;
        public string Type;
        public List<TypeBindMap> Map = new List<TypeBindMap>();

        private List<DataBinder> _binderCaches;

        public virtual void OnEnable()
        {
            if (_binderCaches == null)
            {
                _binderCaches = new List<DataBinder>();
                var type = TypeCaches.GetTypeByName(Assembly, Type);
                foreach (var map in Map)
                {
                    var key = type.Name + "." + map.Property + "." + Key;
                    var (property, field) = TypeCaches.GetTypePropertyOrFieldByName(map.Target.GetType(), map.TargetProperty);
                    var binder = new RuntimePropertyBinder(Context, key, Direction, map.Target, property, field);
                    _binderCaches.Add(binder);
                }
            }

            foreach (var binder in _binderCaches)
            {
                binder.Bind();
                binder.UpdateTarget();
            }
        }

        public virtual void OnDisable()
        {
            foreach (var binder in _binderCaches)
            {
                binder.UnBind();
            }
        }
    }
}