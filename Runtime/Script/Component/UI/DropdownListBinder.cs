using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Aya.DataBinding
{
    [AddComponentMenu("Data Binding/Dropdown List Binder")]
    public class DropdownListBinder : ComponentBinder<Dropdown, List<Dropdown.OptionData>, RuntimeDropdownListBinder>
    {
        public override bool NeedUpdate => true;
    }

    public class RuntimeDropdownListBinder : DataBinderList<Dropdown, List<Dropdown.OptionData>>
    {
        public override bool NeedUpdate => true;

        public override List<Dropdown.OptionData> Value
        {
            get => Target.options;
            set => Target.options = value;
        }
    }

#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(DropdownListBinder)), UnityEditor.CanEditMultipleObjects]
    public class DropdownListBinderEditor : ComponentBinderEditor<Dropdown, List<Dropdown.OptionData>, RuntimeDropdownListBinder>
    {
    }

#endif
}