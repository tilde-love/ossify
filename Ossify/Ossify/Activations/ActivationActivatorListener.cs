using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Ossify.Activations
{
    public sealed class ActivationActivatorListener : MonoBehaviour
    {
        private CancellationTokenSource cancelOnDisabled;
        
        private CancellationToken CancelOnDisabledToken => (cancelOnDisabled ??= new CancellationTokenSource()).Token;
        
        [SerializeField] private Activation activation;
        
        [SerializeField] private Activation[] toActivate;

        private async void OnEnable()
        {
            var cancelToken = CancelOnDisabledToken;

            Activation.Listener listener = null; 
            
            List<Activation.Reference> refs = new ();
            
            try
            {
                foreach (var active in toActivate)
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
                    
                    foreach (var r in refs)
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
                
                foreach (var r in refs)
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