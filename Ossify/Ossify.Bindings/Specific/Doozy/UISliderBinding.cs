#if OSSIFY_DOOZYUI
using System;
using Doozy.Runtime.UIManager.Components;

namespace Ossify.Bindings.Specific.Doozy
{
    public class UISliderBinding : GetterSetterBinding<UISlider, float>
    {
        /// <inheritdoc />
        public UISliderBinding(UISlider bound, Func<float> getter, Action<float> setter) : base(bound, getter, setter)
        {
            Bound.OnValueChanged.AddListener(InvokeValueChanged);
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            Bound.OnValueChanged.RemoveListener(InvokeValueChanged);
        }

        /// <inheritdoc />
        protected override void SetValue(float value)
        {
            Bound.SetValueWithoutNotify(value);
        }
    }
}
#endif