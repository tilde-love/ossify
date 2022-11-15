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

        public T Value
        {
            get => value;
            set
            {
                this.value = value;

                ValueChanged?.Invoke(value);
            }
        }

        private void OnEditorValueChanged()
        {
            if (Application.isPlaying)
            {
                ValueChanged?.Invoke(value);
            }
        }
    }
}