#if OSSIFY_DOOZYUI
using System;
using Doozy.Runtime.UIManager.Components;

namespace Ossify.Bindings.Specific.Doozy
{
    public class UIToggleBinding : GetterSetterBinding<UIToggle, bool>
    {
        /// <inheritdoc />
        public UIToggleBinding(UIToggle bound, Func<bool> getter, Action<bool> setter) : base(bound, getter, setter)
        {
            Bound.OnValueChangedCallback.AddListener(CheckAndInvokeValueChanged);
        }

        /// <inheritdoc />
        public override void Dispose() => Bound.OnValueChangedCallback.RemoveListener(CheckAndInvokeValueChanged);

        /// <inheritdoc />
        protected override void SetValue(bool value)
        {
            if (Bound.isOn != value)
            {
                Bound.SetIsOn(value);
            }
        }

        private void CheckAndInvokeValueChanged(bool value)
        {
            if (Value != value)
            {
                InvokeValueChanged(value);
            }
        }
    }
}
#endif