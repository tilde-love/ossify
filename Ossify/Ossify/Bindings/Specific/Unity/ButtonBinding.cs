using System;
using UnityEngine.UI;

namespace Ossify.Bindings.Specific.Unity
{
    public class ButtonBinding : MethodBinding<Button>
    {
        public ButtonBinding(Button bound, Action action) : base(bound, action) 
        {
            Bound.onClick.AddListener(Invoke);
        }
        
        /// <inheritdoc />
        public override void Dispose() => Bound.onClick.RemoveListener(Invoke);
    }
}