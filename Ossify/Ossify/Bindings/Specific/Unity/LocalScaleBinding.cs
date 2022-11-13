using System;
using UnityEngine;

namespace Ossify.Bindings.Specific.Unity
{
    public class LocalScaleBinding : GetterBinding<Transform, float>
    {
        /// <inheritdoc />
        public LocalScaleBinding(Transform bound, Func<float> getter)
            : base(bound, getter)
        {
        }

        /// <inheritdoc />
        protected override void SetValue(float value) => Bound.localScale = new Vector3(value, value, value);

        /// <inheritdoc />
        public override void Dispose()
        {
        }
    }
}