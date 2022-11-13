using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ossify.Bindings.Specific.Unity
{
    public class ImageBinding : GetterBinding<Image, Sprite>
    {
        /// <inheritdoc />
        public ImageBinding(Image bound, Func<Sprite> getter)
            : base(bound, getter)
        {
        } 

        /// <inheritdoc />
        protected override void SetValue(Sprite value) => Bound.sprite = value;

        /// <inheritdoc />
        public override void Dispose()
        {
        } 
    }
}