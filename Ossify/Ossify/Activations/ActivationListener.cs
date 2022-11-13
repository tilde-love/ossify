using System;
using UnityEngine;
using UnityEngine.Events;

namespace Ossify.Activations
{
    public sealed class ActivationListener : MonoBehaviour
    {
        [SerializeField] private Activation activation;

        [SerializeField] private bool invert;
        private bool isBeingDestroyed;

        private Activation.Listener listener;

        private void Awake() => GetListener();

        private void OnEnable()
        {
            GetListener();

            if (activation != null)
            {
                OnActivationChanged(activation.Active);
            }
        }

        private void OnDestroy()
        {
            isBeingDestroyed = true;

            DisposeListener();

            isBeingDestroyed = false;
        }

        private void GetListener()
        {
            if (listener is { IsExpired: true })
            {
                DisposeListener();
            }

            if (listener != null)
            {
                return;
            }

            listener = activation != null ? activation.GetListener() : null;

            if (listener == null)
            {
                return;
            }

            listener.Changed += OnActivationChanged;
            listener.Expired += OnExpired;
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

            if (this != null && isBeingDestroyed == false)
            {
                gameObject.SetActive(true);
            }
        }

        private void OnActivationChanged(bool active) => gameObject.SetActive(active ^ invert);
    }

    [Serializable]
    public class BoolEvent : UnityEvent<bool>
    {
    }
}