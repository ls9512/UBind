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
                Container = Container,
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
}
