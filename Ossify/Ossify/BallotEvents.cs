using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Ossify
{
    public sealed class BallotEvents : MonoBehaviour
    {
        [SerializeField] private Ballot ballot;

        [SerializeField] private BoolEvent onBallotChanged = new();

        [SerializeField] private UnityEvent onActivated = new();

        [SerializeField] private UnityEvent onDeactivated = new();

        private Ballot.Listener listener;

        private void OnEnable()
        {
            GetListener();

            OnChanged(ballot.Active);
        }

        private void OnDisable()
        {
            DisposeListener();
        }

        private void GetListener()
        {
            if (listener is { Disposed: true }) DisposeListener();

            if (listener != null) return;

            listener = ballot.GetListener();

            listener.Changed += OnChanged;
            listener.Expired += OnExpired;
        }

        private void DisposeListener()
        {
            listener?.Dispose();
            listener = null;
        }

        private void OnExpired(Ballot.Listener listener)
        {
            listener.Changed -= OnChanged;
            listener.Expired -= OnExpired;
        }

        private void OnChanged(bool active)
        {
            onBallotChanged.Invoke(active);

            if (active) onActivated.Invoke();
            else onDeactivated.Invoke();
        }
    }
}