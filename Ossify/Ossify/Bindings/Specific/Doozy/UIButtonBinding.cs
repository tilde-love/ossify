#if OSSIFY_DOOZYUI
using System;
using Doozy.Runtime.Signals;
using Doozy.Runtime.UIManager;
using Doozy.Runtime.UIManager.Components;

namespace Ossify.Bindings.Specific.Doozy
{
    public class UIButtonBinding : MethodBinding<UIButton>
    {
        private readonly SignalReceiver signalReceiver;

        public UIButtonBinding(UIButton bound, Action action) : base(bound, action)
        {
            //initialize the receiver and set its callback
            signalReceiver = new SignalReceiver().SetOnSignalCallback(OnSignal);

            UIButton.stream.ConnectReceiver(signalReceiver);
        }

        /// <inheritdoc />
        public override void Dispose() => UIButton.stream.DisconnectReceiver(signalReceiver);

        private void OnSignal(Signal arg0)
        {
            if (arg0.TryGetValue(out UIButtonSignalData data) == false)
            {
                return;
            }

            if (data.button != Bound)
            {
                return;
            }

            Invoke();
        }
    }
}
#endif