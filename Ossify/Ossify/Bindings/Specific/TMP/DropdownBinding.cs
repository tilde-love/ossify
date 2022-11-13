using System;
using System.Collections.Generic;
using TMPro;

namespace Ossify.Bindings.Specific.TMP
{
    public class DropdownBinding<TEnum> : GetterSetterBinding<TMP_Dropdown, TEnum> where TEnum : Enum
    {
        private readonly List<TEnum> values = new List<TEnum>();

        /// <inheritdoc />
        public DropdownBinding(TMP_Dropdown bound, Func<TEnum> getter, Action<TEnum> setter) : base(bound, getter, setter)
        {
            Bound.options.Clear();
            values.Clear();

            foreach (var value in Enum.GetValues(typeof(TEnum)))
            {
                values.Add((TEnum)value);
                Bound.options.Add(new TMP_Dropdown.OptionData(value.ToString()));
            }

            Bound.onValueChanged.AddListener(InvokeValueChanged);
        }

        /// <inheritdoc />
        protected override void SetValue(TEnum value) => Bound.SetValueWithoutNotify(values.IndexOf(value));

        /// <inheritdoc />
        public override void Dispose() => Bound.onValueChanged.RemoveListener(InvokeValueChanged);

        private void InvokeValueChanged(int value) => InvokeValueChanged(values[value]);
    }
}