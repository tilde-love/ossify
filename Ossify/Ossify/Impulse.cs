using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Ossify.Ballots
{
    [CreateAssetMenu(order = Consts.ActivationMenuItems, menuName = "Variables/Impulse")]
    public sealed class Impulse : ScriptableObject
    {
        public event Action Pulsed;

        [Button]
        public void Invoke() => Pulsed?.Invoke();

        public async UniTask Wait(CancellationToken cancellationToken)
        {
            bool pulsed = false;

            void OnPulsed()
            {
                pulsed = true;
            }

            Pulsed += OnPulsed;

            try
            {
                await UniTask.WaitUntil(() => pulsed, cancellationToken: cancellationToken);
            }
            finally
            {
                Pulsed -= OnPulsed;
            }
        }
    }

    public abstract class Impulse<T0> : ScriptableObject
    {
        public event Action<T0> Pulsed;

        [Button]
        public void Invoke(T0 v0) => Pulsed?.Invoke(v0);

        public async UniTask<T0> Wait(CancellationToken cancellationToken)
        {
            bool pulsed = false;

            T0 v0 = default;

            void OnPulsed(T0 a0)
            {
                v0 = a0;
                pulsed = true;
            }

            Pulsed += OnPulsed;

            try
            {
                await UniTask.WaitUntil(() => pulsed, cancellationToken: cancellationToken);

                return v0;
            }
            finally
            {
                Pulsed -= OnPulsed;
            }
        }
    }

    public abstract class ImpulseListener<T0> : MonoBehaviour
    {
        [FormerlySerializedAs("Pulse")] public Impulse<T0> impulse;

        public UnityEvent<T0> OnPulsed = new ImpulseUnityEvent();
        private CancellationTokenSource cancelOnDisabled;

        private CancellationToken CancelOnDisabledToken => (cancelOnDisabled ??= new CancellationTokenSource()).Token;

        private async void OnEnable()
        {
            CancellationToken cancel = CancelOnDisabledToken;

            while (cancel.IsCancellationRequested == false)
            {
                T0 v0 = await impulse.Wait(cancel);

                OnPulsed.Invoke(v0);
            }
        }

        private void OnDisable()
        {
            cancelOnDisabled?.Cancel();
            cancelOnDisabled?.Dispose();
            cancelOnDisabled = null;
        }

        private sealed class ImpulseUnityEvent : UnityEvent<T0>
        {
        }
    }

    public abstract class Impulse<T0, T1> : ScriptableObject
    {
        public event Action<T0, T1> Pulsed;

        [Button]
        public void Invoke(T0 v0, T1 v1) => Pulsed?.Invoke(v0, v1);

        public async UniTask<(T0 v0, T1 v1)> Wait(CancellationToken cancellationToken)
        {
            bool pulsed = false;

            (T0 v0, T1 v1) result = (default, default);

            void OnPulsed(T0 a0, T1 a1)
            {
                result = (a0, a1);
                pulsed = true;
            }

            Pulsed += OnPulsed;

            try
            {
                await UniTask.WaitUntil(() => pulsed, cancellationToken: cancellationToken);

                return result;
            }
            finally
            {
                Pulsed -= OnPulsed;
            }
        }
    }

    public abstract class ImpulseListener<T0, T1> : MonoBehaviour
    {
        [FormerlySerializedAs("Pulse")] public Impulse<T0, T1> impulse;

        public UnityEvent<T0, T1> OnPulsed = new ImpulseUnityEvent();
        private CancellationTokenSource cancelOnDisabled;

        private CancellationToken CancelOnDisabledToken => (cancelOnDisabled ??= new CancellationTokenSource()).Token;

        private async void OnEnable()
        {
            CancellationToken cancel = CancelOnDisabledToken;

            while (cancel.IsCancellationRequested == false)
            {
                (T0 v0, T1 v1) = await impulse.Wait(cancel);

                OnPulsed.Invoke(v0, v1);
            }
        }

        private void OnDisable()
        {
            cancelOnDisabled?.Cancel();
            cancelOnDisabled?.Dispose();
            cancelOnDisabled = null;
        }

        private sealed class ImpulseUnityEvent : UnityEvent<T0, T1>
        {
        }
    }

    public abstract class Impulse<T0, T1, T2> : ScriptableObject
    {
        public event Action<T0, T1, T2> Pulsed;

        [Button]
        public void Invoke(T0 v0, T1 v1, T2 v2) => Pulsed?.Invoke(v0, v1, v2);

        public async UniTask<(T0 v0, T1 v1, T2 v2)> Wait(CancellationToken cancellationToken)
        {
            bool pulsed = false;

            (T0 v0, T1 v1, T2 v2) result = (default, default, default);

            void OnPulsed(T0 a0, T1 a1, T2 a2)
            {
                result = (a0, a1, a2);
                pulsed = true;
            }

            Pulsed += OnPulsed;

            try
            {
                await UniTask.WaitUntil(() => pulsed, cancellationToken: cancellationToken);

                return result;
            }
            finally
            {
                Pulsed -= OnPulsed;
            }
        }
    }

    public abstract class ImpulseListener<T0, T1, T2> : MonoBehaviour
    {
        [FormerlySerializedAs("Pulse")] public Impulse<T0, T1, T2> impulse;

        public UnityEvent<T0, T1, T2> OnPulsed = new ImpulseUnityEvent();
        private CancellationTokenSource cancelOnDisabled;

        private CancellationToken CancelOnDisabledToken => (cancelOnDisabled ??= new CancellationTokenSource()).Token;

        private async void OnEnable()
        {
            CancellationToken cancel = CancelOnDisabledToken;

            while (cancel.IsCancellationRequested == false)
            {
                (T0 v0, T1 v1, T2 v2) = await impulse.Wait(cancel);

                OnPulsed.Invoke(v0, v1, v2);
            }
        }

        private void OnDisable()
        {
            cancelOnDisabled?.Cancel();
            cancelOnDisabled?.Dispose();
            cancelOnDisabled = null;
        }

        private sealed class ImpulseUnityEvent : UnityEvent<T0, T1, T2>
        {
        }
    }

    public abstract class Impulse<T0, T1, T2, T3> : ScriptableObject
    {
        public event Action<T0, T1, T2, T3> Pulsed;

        [Button]
        public void Invoke(
            T0 v0,
            T1 v1,
            T2 v2,
            T3 v3) => Pulsed?.Invoke(v0, v1, v2, v3);

        public async UniTask<(T0 v0, T1 v1, T2 v2, T3 v3)> Wait(CancellationToken cancellationToken)
        {
            bool pulsed = false;

            (T0 v0, T1 v1, T2 v2, T3 v3) result = (default, default, default, default);

            void OnPulsed(
                T0 a0,
                T1 a1,
                T2 a2,
                T3 a3)
            {
                result = (a0, a1, a2, a3);
                pulsed = true;
            }

            Pulsed += OnPulsed;

            try
            {
                await UniTask.WaitUntil(() => pulsed, cancellationToken: cancellationToken);

                return result;
            }
            finally
            {
                Pulsed -= OnPulsed;
            }
        }
    }

    public abstract class ImpulseListener<T0, T1, T2, T3> : MonoBehaviour
    {
        [FormerlySerializedAs("Pulse")] public Impulse<T0, T1, T2, T3> impulse;

        public UnityEvent<T0, T1, T2, T3> OnPulsed = new ImpulseUnityEvent();
        private CancellationTokenSource cancelOnDisabled;

        private CancellationToken CancelOnDisabledToken => (cancelOnDisabled ??= new CancellationTokenSource()).Token;

        private async void OnEnable()
        {
            CancellationToken cancel = CancelOnDisabledToken;

            while (cancel.IsCancellationRequested == false)
            {
                (T0 v0, T1 v1, T2 v2, T3 v3) = await impulse.Wait(cancel);

                OnPulsed.Invoke(v0, v1, v2, v3);
            }
        }

        private void OnDisable()
        {
            cancelOnDisabled?.Cancel();
            cancelOnDisabled?.Dispose();
            cancelOnDisabled = null;
        }

        private sealed class ImpulseUnityEvent : UnityEvent<T0, T1, T2, T3>
        {
        }
    }
}