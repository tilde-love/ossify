using System.Threading;
using UnityEngine;
using UnityEngine.Events;

namespace Ossify.Activations
{
    public sealed class PulseListener : MonoBehaviour
    {
        public Pulse Pulse;

        public UnityEvent OnPulsed = new();
        private CancellationTokenSource cancelOnDisabled;

        private CancellationToken CancelOnDisabledToken => (cancelOnDisabled ??= new CancellationTokenSource()).Token;

        private async void OnEnable()
        {
            CancellationToken cancel = CancelOnDisabledToken;

            while (cancel.IsCancellationRequested == false)
            {
                await Pulse.Wait(cancel);

                OnPulsed.Invoke();
            }
        }

        private void OnDisable()
        {
            cancelOnDisabled?.Cancel();
            cancelOnDisabled?.Dispose();
            cancelOnDisabled = null;
        }
    }
}