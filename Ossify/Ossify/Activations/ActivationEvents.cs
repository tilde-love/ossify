using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Ossify.Activations
{
    public sealed class ActivationEvents : MonoBehaviour
    {
        [SerializeField] private Activation activation;

        [FormerlySerializedAs("onActivation"), SerializeField]
        private BoolEvent onActivationChanged = new();

        [SerializeField] private UnityEvent onActivated = new();
        [SerializeField] private UnityEvent onDeactivated = new();

        private Activation.Listener listener;

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

        private void OnActivationChanged(bool active)
        {
            onActivationChanged.Invoke(active);

            if (active)
            {
                onActivated.Invoke();
            }
            else
            {
                onDeactivated.Invoke();
            }
        }
    }
}