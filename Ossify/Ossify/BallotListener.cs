using System;
using UnityEngine;
using UnityEngine.Events;

namespace Ossify
{
    public sealed class BallotListener : MonoBehaviour
    {
        [SerializeField] private Ballot ballot;

        [SerializeField] private bool invert;

        private bool isBeingDestroyed;

        private Ballot.Listener listener;

        private void Awake() => GetListener();

        private void OnEnable()
        {
            GetListener();

            if (ballot != null) OnActivationChanged(ballot.Active);
        }

        private void OnDestroy()
        {
            isBeingDestroyed = true;

            DisposeListener();

            isBeingDestroyed = false;
        }

        private void GetListener()
        {
            if (listener is { Disposed: true }) DisposeListener();

            if (listener != null) return;

            listener = ballot != null ? ballot.GetListener() : null;

            if (listener == null) return;

            listener.Changed += OnActivationChanged;
            listener.Expired += OnExpired;
        }

        private void DisposeListener()
        {
            listener?.Dispose();
            listener = null;
        }

        private void OnExpired(Ballot.Listener listener)
        {
            listener.Changed -= OnActivationChanged;
            listener.Expired -= OnExpired;

            if (this != null && isBeingDestroyed == false) gameObject.SetActive(true);
        }

        private void OnActivationChanged(bool active) => gameObject.SetActive(active ^ invert);
    }

    [Serializable]
    public class BoolEvent : UnityEvent<bool> { }
}