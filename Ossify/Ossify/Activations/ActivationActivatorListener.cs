using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Ossify.Activations
{
    public sealed class ActivationActivatorListener : MonoBehaviour
    {
        [SerializeField] private Activation activation;

        [SerializeField] private Activation[] toActivate;
        private CancellationTokenSource cancelOnDisabled;

        private CancellationToken CancelOnDisabledToken => (cancelOnDisabled ??= new CancellationTokenSource()).Token;

        private async void OnEnable()
        {
            CancellationToken cancelToken = CancelOnDisabledToken;

            Activation.Listener listener = null;

            List<Activation.Reference> refs = new();

            try
            {
                foreach (Activation active in toActivate)
                {
                    refs.Add(active.GetReference(false));
                }

                while (cancelToken.IsCancellationRequested == false)
                {
                    if (listener?.IsExpired ?? true)
                    {
                        listener?.Dispose();

                        listener = activation.GetListener();
                    }

                    bool state = listener.Active;

                    foreach (Activation.Reference r in refs)
                    {
                        r.Active = state;
                    }

                    await UniTask.WaitUntil(() => listener.Active != state || listener.IsExpired, cancellationToken: cancelToken);
                }
            }
            catch (Exception ex) when (ex.IsCancellation())
            {
            }
            finally
            {
                listener?.Dispose();

                foreach (Activation.Reference r in refs)
                {
                    r.Dispose();
                }

                refs.Clear();
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