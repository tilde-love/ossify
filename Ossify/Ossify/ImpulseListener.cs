using System.Threading;
using UnityEngine;
using UnityEngine.Events;

namespace Ossify
{
    public sealed class ImpulseListener : AsyncMonoBehaviour
    {
        [SerializeField] private Impulse impulse;

        public UnityEvent OnPulsed = new();

        private async void OnEnable()
        {
            CancellationToken cancel = CancellationToken;

            while (cancel.IsCancellationRequested == false)
            {
                await impulse.Wait(cancel);

                OnPulsed.Invoke();
            }
        }
    }
}