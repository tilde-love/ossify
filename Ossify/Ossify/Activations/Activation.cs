using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Ossify.Activations
{
    [CreateAssetMenu(order = Consts.ActivationMenuItems, menuName = "Variables/Activation")]
    public sealed class Activation : ScriptableObject
    {
        [SerializeField, TextArea] private string comment;

        private readonly List<Listener> listeners = new();

        [NonSerialized] private int active;

        [NonSerialized] private readonly List<IDisposable> references = new();

        [ShowInInspector]
        public bool Active => active > 0;

        private void OnDisable()
        {
            ClearReferences();

            ClearListeners();
        }

        public event Action<bool> ActiveChanged;

        public Listener GetListener()
        {
            Listener listener = new Listener(this);

            listener.Expired += ListenerOnExpired;

            listeners.Add(listener);

            return listener;
        }

        private void ListenerOnExpired(Listener listener)
        {
            listener.Expired -= ListenerOnExpired;

            listeners.Remove(listener);
        }

        private void ClearListeners()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].Dispose();
            }

            listeners.Clear();
        }

        private void Invoke(bool value)
        {
            ActiveChanged?.Invoke(value);

            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].ValueChanged(value);
            }
        }

        public Reference GetReference(bool active = true) => AddReference(new Reference(this, active));

        private void ClearReferences()
        {
            for (int index = references.Count - 1; index >= 0; index--)
            {
                IDisposable disposable = references[index];

                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
        }

        private Reference AddReference(Reference reference)
        {
            references.Add(reference);

            return reference;
        }

        private void RemoveReference(IDisposable reference) => references.Remove(reference);

        private void Decrement()
        {
            if (Interlocked.Decrement(ref active) == 0)
            {
                Invoke(Active);
            }
        }

        private void Increment()
        {
            if (Interlocked.Increment(ref active) == 1)
            {
                Invoke(Active);
            }
        }

        public sealed class Listener : IDisposable
        {
            private readonly Activation source;
            
            private readonly CancellationTokenSource cancellationTokenSource = new ();

            public CancellationToken CancellationToken => cancellationTokenSource.Token;
            
            public bool IsExpired { get; private set; }
            
            public bool Active => source.Active;

            internal Listener(Activation source) => this.source = source;

            public void Dispose()
            {
                if (IsExpired)
                {
                    return;
                }

                IsExpired = true;

                cancellationTokenSource.Cancel();
                
                Expired?.Invoke(this);
            }

            public event Action<bool> Changed;

            public event Action<Listener> Expired;

            internal void ValueChanged(bool value)
            {
                if (IsExpired)
                {
                    return;
                }

                Changed?.Invoke(value);
            }
        }

        public sealed class Reference : IDisposable
        {
            private readonly Activation activation;
            private bool active;

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
                        activation.Increment();
                    }
                    else
                    {
                        activation.Decrement();
                    }
                }
            }

            public bool Disposed { get; private set; }

            internal Reference(Activation activation, bool active)
            {
                this.activation = activation;
                this.active = active;

                if (active)
                {
                    activation.Increment();
                }
            }

            public void Dispose()
            {
                if (Disposed)
                {
                    return;
                }

                Disposed = true;

                if (active)
                {
                    activation.Decrement();
                }

                activation.RemoveReference(this);
            }
        }

        public async UniTask WaitUntilActive(CancellationToken cancelOnDisableToken)
        {
            if (Active)
            {
                return;
            }
            
            using Listener listener = GetListener();

            using CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancelOnDisableToken, listener.CancellationToken);

            await UniTask.WaitUntil(() => Active, cancellationToken: cancellationTokenSource.Token);
        }
        
        public async UniTask WaitUntilInactive(CancellationToken cancelOnDisableToken)
        {
            if (Active == false)
            {
                return;
            }
            
            using Listener listener = GetListener();

            using CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancelOnDisableToken, listener.CancellationToken);

            await UniTask.WaitUntil(() => Active == false, cancellationToken: cancellationTokenSource.Token);
        }
    }
}