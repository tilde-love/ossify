using System.Threading;
using UnityEngine;
using UnityEngine.Events;

namespace Ossify.Activations
{
    public sealed class PulseListener : MonoBehaviour
    {
        private CancellationTokenSource cancelOnDisabled;
        
        private CancellationToken CancelOnDisabledToken => (cancelOnDisabled ??= new CancellationTokenSource()).Token;

        public Pulse Pulse;

        public UnityEvent OnPulsed = new UnityEvent();

        async void OnEnable()
        {
            var cancel = CancelOnDisabledToken;

            while (cancel.IsCancellationRequested == false)
            {
                await Pulse.Wait(cancel);
                
                OnPulsed.Invoke();
            }
        }

        void OnDisable()
        {
            cancelOnDisabled?.Cancel();
            cancelOnDisabled?.Dispose();
            cancelOnDisabled = null;
        }
    }
}