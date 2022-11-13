using System;
using UnityEngine.UI;

namespace Ossify.Bindings.Specific.Unity
{
    public class SliderBinding : GetterSetterBinding<Slider, float>
    {
        /// <inheritdoc />
        public SliderBinding(Slider bound, Func<float> getter, Action<float> setter) 
            : base(bound, getter, setter) =>
            Bound.onValueChanged.AddListener(InvokeValueChanged);

        /// <inheritdoc />
        protected override void SetValue(float value) => Bound.SetValueWithoutNotify(value);

        /// <inheritdoc />
        public override void Dispose() => Bound.onValueChanged.RemoveListener(InvokeValueChanged);
    }
}