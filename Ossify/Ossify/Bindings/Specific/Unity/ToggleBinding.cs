using System;
using UnityEngine.UI;

namespace Ossify.Bindings.Specific.Unity
{
    public class ToggleBinding : GetterSetterBinding<Toggle, bool>
    {
        /// <inheritdoc />
        public ToggleBinding(Toggle bound, Func<bool> getter, Action<bool> setter)
            : base(bound, getter, setter) =>
            Bound.onValueChanged.AddListener(InvokeValueChanged);

        /// <inheritdoc />
        public override void Dispose() => Bound.onValueChanged.RemoveListener(InvokeValueChanged);

        /// <inheritdoc />
        protected override void SetValue(bool value) => Bound.SetIsOnWithoutNotify(value);
    }
}