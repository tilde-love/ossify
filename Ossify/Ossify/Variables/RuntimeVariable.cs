using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Ossify.Variables
{
    public abstract class RuntimeVariable<T> : ScriptableObject, IVariable<T> where T : class
    {
        [SerializeField, TextArea] private string comment;

        private T value;

        public event Action<T> ValueChanged;

        /// <inheritdoc />
        Type IVariable.ValueType => typeof(T);

        /// <inheritdoc />
        object IVariable.Value
        {
            get => Value;
            set => Value = (T)value;
        }

        /// <inheritdoc />
        event Action<object> IVariable.ValueChanged
        {
            add => baseValueChanged += value;
            remove => baseValueChanged -= value;
        }

        [ShowInInspector, OnValueChanged(nameof(OnEditorValueChanged))]
        public T Value
        {
            get => value;
            set
            {
                this.value = value;

                ValueChanged?.Invoke(value);
                baseValueChanged?.Invoke(value);
            }
        }

        private event Action<object> baseValueChanged;

        private void OnEditorValueChanged()
        {
            if (Application.isPlaying == false)
            {
                return;
            }

            ValueChanged?.Invoke(value);
            baseValueChanged?.Invoke(value);
        }

        public async UniTask<T> WaitForValue(CancellationToken cancellationToken)
        {
            if (Value != null)
            {
                return Value;
            }

            await UniTask.WaitUntil(() => Value != null, cancellationToken: cancellationToken);

            return Value;
        }
    }
}