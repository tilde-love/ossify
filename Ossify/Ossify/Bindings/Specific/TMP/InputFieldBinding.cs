using System;
using TMPro;

namespace Ossify.Bindings.Specific.TMP
{
    public class InputFieldBinding : GetterSetterBinding<TMP_InputField, string>
    {
        /// <inheritdoc />
        public InputFieldBinding(TMP_InputField bound, Func<string> getter, Action<string> setter) : base(bound, getter, setter)
        {
            Bound.onValueChanged.AddListener(InvokeValueChanged);
        }

        /// <inheritdoc />
        protected override void SetValue(string value) => Bound.SetTextWithoutNotify(value);

        /// <inheritdoc />
        public override void Dispose() => Bound.onValueChanged.RemoveListener(InvokeValueChanged);
    }
}