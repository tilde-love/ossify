using UnityEngine;

namespace Ossify.Activations
{
    public sealed class ActivationListenerEvent : MonoBehaviour
    {
        private Activation.Listener listener; 
        
        [SerializeField] private Activation activation;
        
        [SerializeField] private bool invert;

        [SerializeField] private BoolEvent onActivation = new ();

        private void OnEnable()
        {
            GetListener();
            
            OnActivationChanged(activation.Active);
        }
        
        private void OnDisable() => DisposeListener(); 
        
        private void GetListener()
        {
            if (listener is { IsExpired: true })
            {
                DisposeListener();
            }

            if (listener == null)
            {
                listener = activation.GetListener();
                
                listener.Changed += OnActivationChanged;
                listener.Expired += OnExpired;
                
            }
        }
        
        private void DisposeListener()
        {
            listener?.Dispose();
            listener = null;
        }

        private void OnExpired(Activation.Listener listener)
        {
            listener.Changed -= OnActivationChanged;
            listener.Expired -= OnExpired;
        }
        
        private void OnActivationChanged(bool active) => onActivation.Invoke(active ^ invert);
    }
}