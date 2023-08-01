using System;
using System.Threading;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Ossify.Ballots
{
    [CreateAssetMenu(order = Consts.ActivationMenuItems, menuName = "Variables/Ballot")]
    public sealed class Ballot : ScriptableObject
    {
        public enum BallotThresholdType
        {
            AboveNumber,
            BelowNumber,
            AboveProportion,
            BelowProportion
        }

        [SerializeField, TextArea] private string comment;

        [SerializeField, OnValueChanged(nameof(MaintainActiveState))] private BallotThresholdType thresholdType = BallotThresholdType.AboveNumber;

        [ShowIf(nameof(IsNumberThreshold)), SerializeField, LabelText("Threshold")]
        private int numberThreshold = 1;

        [HideIf(nameof(IsNumberThreshold)), SerializeField, LabelText("Threshold"), Range(0, 1)]
        private float proportionThreshold = 0.5f;

        [NonSerialized] private readonly Custodian<Listener> listeners;

        [NonSerialized] private readonly Custodian<Reference, bool> references;

        [NonSerialized] private int active;

        [NonSerialized] private bool currentActive;

        public Ballot()
        {
            references = new Custodian<Reference, bool>(IssueReference);
            listeners = new Custodian<Listener>(IssueListener);
        }

        [ShowInInspector]
        public bool Active => TestThreshold();

        private void OnDisable()
        {
            references.Clear();

            listeners.Clear();
        }

        private bool TestThreshold()
        {
            switch (thresholdType)
            {
                case BallotThresholdType.AboveNumber: return active > numberThreshold;
                case BallotThresholdType.BelowNumber: return active < numberThreshold;
                case BallotThresholdType.AboveProportion: return active > references.Count * proportionThreshold;
                case BallotThresholdType.BelowProportion: return active < references.Count * proportionThreshold;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public event Action<bool> ActiveChanged;

        public Listener GetListener() => listeners.Get();

        public Reference GetReference(bool active) => references.Get(active);

        private bool IsNumberThreshold()
            => thresholdType == BallotThresholdType.AboveNumber || thresholdType == BallotThresholdType.BelowNumber;

        private Reference IssueReference(bool active, Custodian<Reference, bool>.Destructor destructor)
            => new(this, active, destructor);

        private Listener IssueListener(Custodian<Listener>.Destructor destructor)
            => new(this, destructor);

        private void Invoke(bool value)
        {
            ActiveChanged?.Invoke(value);

            for (int i = listeners.Count - 1; i >= 0; i--) listeners[i].ValueChanged(value);
        }

        private void Decrement()
        {
            Interlocked.Decrement(ref active);

            MaintainActiveState();
        }

        private void Increment()
        {
            Interlocked.Increment(ref active);

            MaintainActiveState();
        }

        private void MaintainActiveState() 
        {
            if (currentActive == Active) return;

            currentActive = Active;
            
            Invoke(currentActive);
        }

        // public async UniTask WaitUntilActive(CancellationToken cancelOnDisableToken)
        // {
        //     if (Active)
        //     {
        //         return;
        //     }
        //
        //     using Listener listener = GetListener();
        //
        //     using CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancelOnDisableToken, listener.CancellationToken);
        //
        //     await UniTask.WaitUntil(() => Active, cancellationToken: cancellationTokenSource.Token);
        // }

        // public async UniTask WaitUntilInactive(CancellationToken cancelOnDisableToken)
        // {
        //     if (Active == false)
        //     {
        //         return;
        //     }
        //
        //     using Listener listener = GetListener();
        //
        //     using CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancelOnDisableToken, listener.CancellationToken);
        //
        //     await UniTask.WaitUntil(() => Active == false, cancellationToken: cancellationTokenSource.Token);
        // }

        public sealed class Listener : IArtifact
        {
            private readonly CancellationTokenSource cancellationTokenSource = new();
            private readonly Custodian<Listener>.Destructor destructor;
            private readonly Ballot source;

            public bool Active => source.Active;

            public CancellationToken CancellationToken => cancellationTokenSource.Token;

            internal Listener(Ballot source, Custodian<Listener>.Destructor destructor)
            {
                this.source = source;
                this.destructor = destructor;
            }

            /// <inheritdoc />
            public bool Disposed { get; private set; }

            public void Dispose()
            {
                if (Disposed)
                {
                    return;
                }

                Disposed = true;

                cancellationTokenSource.Cancel();

                destructor(this);

                Expired?.Invoke(this);
            }

            public event Action<bool> Changed;

            public event Action<Listener> Expired;

            internal void ValueChanged(bool value)
            {
                if (Disposed)
                {
                    return;
                }

                Changed?.Invoke(value);
            }
        }

        public sealed class Reference : IArtifact
        {
            private bool active;
            private readonly Ballot ballot;
            private readonly Custodian<Reference, bool>.Destructor destructor;

            public bool Active
            {
                get => active;
                set
                {
                    if (active == value)
                    {
                        return;
                    }

                    active = value;

                    if (active)
                    {
                        ballot.Increment();
                    }
                    else
                    {
                        ballot.Decrement();
                    }
                }
            }

            internal Reference(Ballot ballot, bool active, Custodian<Reference, bool>.Destructor destructor)
            {
                this.destructor = destructor;
                this.ballot = ballot;
                this.active = active;

                if (active)
                {
                    ballot.Increment();
                }
            }

            public bool Disposed { get; private set; }

            public void Dispose()
            {
                if (Disposed)
                {
                    return;
                }

                Disposed = true;

                if (active)
                {
                    ballot.Decrement();
                }

                destructor(this);
            }
        }
    }
}