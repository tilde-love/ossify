using System;
using System.Collections.Generic;
using TMPro;

namespace Ossify.Bindings.Specific.TMP
{
    public class DropdownBinding<TEnum> : GetterSetterBinding<TMP_Dropdown, TEnum> where TEnum : Enum
    {
        private readonly List<TEnum> values = new();

        /// <inheritdoc />
        public DropdownBinding(TMP_Dropdown bound, Func<TEnum> getter, Action<TEnum> setter) : base(bound, getter, setter)
        {
            Bound.options.Clear();
            values.Clear();

            foreach (object value in Enum.GetValues(typeof(TEnum)))
            {
                values.Add((TEnum)value);
                Bound.options.Add(new TMP_Dropdown.OptionData(value.ToString()));
            }

            Bound.onValueChanged.AddListener(InvokeValueChanged);
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            Bound.onValueChanged.RemoveListener(InvokeValueChanged);
        }

        /// <inheritdoc />
        protected override void SetValue(TEnum value)
        {
            Bound.SetValueWithoutNotify(values.IndexOf(value));
        }

        private void InvokeValueChanged(int value)
        {
            InvokeValueChanged(values[value]);
        }
    }
}