using System;
using UnityEngine.UI;

namespace Ossify.Bindings.Specific.Unity
{
    public class FilledImageBinding : GetterBinding<Image, float>
    {
        /// <inheritdoc />
        public FilledImageBinding(Image bound, Func<float> getter)
            : base(bound, getter) { }

        /// <inheritdoc />
        public override void Dispose() { }

        /// <inheritdoc />
        protected override void SetValue(float value)
        {
            Bound.fillAmount = value;
        }
    }
}