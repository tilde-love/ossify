using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Ossify.Ballots
{
    public sealed class ImpulseListener : MonoBehaviour
    {
        [FormerlySerializedAs("Pulse")] public Impulse impulse;

        public UnityEvent OnPulsed = new();
        private CancellationTokenSource cancelOnDisabled;

        private CancellationToken CancelOnDisabledToken => (cancelOnDisabled ??= new CancellationTokenSource()).Token;

        private async void OnEnable()
        {
            CancellationToken cancel = CancelOnDisabledToken;

            while (cancel.IsCancellationRequested == false)
            {
                await impulse.Wait(cancel);

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