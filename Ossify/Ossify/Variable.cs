using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ossify
{
    public abstract class Variable<TValue> : ScriptableObject, IVariable<TValue>
    {
        [SerializeField, TextArea] private string comment;

        [HideIf(nameof(ShowVolatileValue)),
         OnValueChanged(nameof(OnEditorChangedProtectedValue)),
         SerializeField,
         HideLabel,
         InlineProperty,
         FormerlySerializedAs("value")]
        private TValue protectedValue;

        [SerializeField] private VariableAccess access = VariableAccess.Volatile;

        private bool hasVolatileValueBeenSet;

        [ShowIf(nameof(ShowVolatileValue)),
         NonSerialized,
         ShowInInspector,
         HideLabel,
         InlineProperty,
         OnValueChanged(nameof(OnEditorChangedVolatileValue))]
        private TValue volatileValue;

        public VariableAccess Access => access;

        public void SetProtectedValue(TValue value) => SetInnerProtectedValue(value);

        void IVariable.SetProtectedValue(object value) => SetInnerProtectedValue((TValue)value);

        public event Action<TValue> ValueChanged;

        /// <inheritdoc />
        Type IVariable.ValueType => typeof(TValue);

        /// <inheritdoc />
        object IVariable.Value
        {
            get => Value;
            set => Value = (TValue)value;
        }

        /// <inheritdoc />
        event Action<object> IVariable.ValueChanged
        {
            add => objectValueChanged += value;
            remove => objectValueChanged -= value;
        }

        public TValue Value
        {
            get => GetValue();
            set => SetValue(value);
        }

        protected bool ShowVolatileValue() => access == VariableAccess.Volatile && Application.isPlaying;

        private void SetValue(TValue value)
        {
            if (ShowVolatileValue() == false) return;

            volatileValue = value;

            TriggerValueChange(value);
        }

        private void TriggerValueChange(TValue value)
        {
            if (Application.isPlaying == false) return;

            ValueChanged?.Invoke(value);

            objectValueChanged?.Invoke(value);

#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }

        private TValue GetValue()
        {
            if (hasVolatileValueBeenSet == false && ShowVolatileValue())
            {
                volatileValue = protectedValue;
                hasVolatileValueBeenSet = true;
            }

            return ShowVolatileValue() ? volatileValue : protectedValue;
        }

        private void SetInnerProtectedValue(TValue value)
        {
            volatileValue = protectedValue = value;

            TriggerValueChange(value);
        }

        private event Action<object> objectValueChanged;

        /// <summary>
        /// The only known use case is when making custom inspectors, its probably not a good idea to use this anywhere else
        /// </summary>
        protected void OnEditorChangedProtectedValue()
        {
            if (Application.isPlaying == false) return;

            volatileValue = protectedValue;

            TriggerValueChange(volatileValue);
        }

        /// <summary>
        /// The only known use case is when making custom inspectors, its probably not a good idea to use this anywhere else
        /// </summary>
        protected void OnEditorChangedVolatileValue() => TriggerValueChange(volatileValue);
    }
}