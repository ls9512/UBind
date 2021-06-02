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

        public override void SetData(List<Dropdown.OptionData> data)
        {
            Target.options = data;
        }

        public override List<Dropdown.OptionData> GetData()
        {
            return Target.options;
        }
    }

#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(DropdownListBinder)), UnityEditor.CanEditMultipleObjects]
    public class DropdownListBinderEditor : ComponentBinderEditor<Dropdown, List<Dropdown.OptionData>, RuntimeDropdownListBinder>
    {
    }

#endif
}