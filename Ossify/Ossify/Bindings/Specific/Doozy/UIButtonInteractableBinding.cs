#if OSSIFY_DOOZYUI
using System;
using Doozy.Runtime.UIManager.Components;

namespace Ossify.Bindings.Specific.Doozy
{
    public class UIButtonInteractableBinding : GetterBinding<UIButton, bool>
    {
        /// <inheritdoc />
        public UIButtonInteractableBinding(UIButton bound, Func<bool> getter) : base(bound, getter) { }

        /// <inheritdoc />
        protected override void SetValue(bool value) => Bound.interactable = value;
    }
}
#endif