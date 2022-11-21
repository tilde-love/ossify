using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Ossify.Variables
{
    public abstract class Variable<T> : ScriptableObject, IVariable<T>
    {
        [SerializeField, TextArea] private string comment;

        [SerializeField, HideLabel, InlineProperty, OnValueChanged(nameof(OnEditorValueChanged))]
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
    }
}