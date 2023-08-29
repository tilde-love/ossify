using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ossify.Microtypes
{
    // 
    // public sealed class StringVariable : Variable<string> { }

    [CreateAssetMenu(order = Consts.VariableOrder, menuName = "Ossify/Text")]
    public sealed class TextVariable : ScriptableObject, IVariable<string>
    {
        [SerializeField, TextArea] private string comment;

        [HideIf(nameof(ShowVolatileValue)),
         OnValueChanged(nameof(OnEditorChangedProtectedValue)),
         SerializeField,
         HideLabel,
         InlineProperty,            
         TextArea(3, 10)]
        private string protectedValue;

        [SerializeField] private VariableAccess access = VariableAccess.Volatile;

        private bool hasVolatileValueBeenSet;

        [ShowIf(nameof(ShowVolatileValue)),
         OnValueChanged(nameof(OnEditorChangedVolatileValue)),
         NonSerialized,
         ShowInInspector,
         HideLabel,
         InlineProperty,
         TextArea(3, 10)]
        private string volatileValue;

        public VariableAccess Access => access;

        public void SetProtectedValue(string value) => SetInnerProtectedValue(value);

        void IVariable.SetProtectedValue(object value) => SetInnerProtectedValue((string)value);

        public event Action<string> ValueChanged;

        /// <inheritdoc />
        Type IVariable.ValueType => typeof(string);

        /// <inheritdoc />
        object IVariable.Value
        {
            get => Value;
            set => Value = (string)value;
        }

        /// <inheritdoc />
        event Action<object> IVariable.ValueChanged
        {
            add => objectValueChanged += value;
            remove => objectValueChanged -= value;
        }

        public string Value
        {
            get => GetValue();
            set => SetValue(value);
        }

        private bool ShowVolatileValue() => access == VariableAccess.Volatile && Application.isPlaying;

        private void SetValue(string value)
        {
            if (ShowVolatileValue() == false) return;

            volatileValue = value;

            TriggerValueChange(value);
        }

        private void TriggerValueChange(string value)
        {
            if (Application.isPlaying == false) return;

            ValueChanged?.Invoke(value);

            objectValueChanged?.Invoke(value);

#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }

        private string GetValue()
        {
            if (hasVolatileValueBeenSet == false && ShowVolatileValue())
            {
                volatileValue = protectedValue;
                hasVolatileValueBeenSet = true;
            }

            return ShowVolatileValue() ? volatileValue : protectedValue;
        }

        private void SetInnerProtectedValue(string value)
        {
            volatileValue = protectedValue = value;

            TriggerValueChange(value);
        }

        private event Action<object> objectValueChanged;

        private void OnEditorChangedProtectedValue()
        {
            if (Application.isPlaying == false) return;

            volatileValue = protectedValue;

            TriggerValueChange(volatileValue);
        }

        private void OnEditorChangedVolatileValue() => TriggerValueChange(volatileValue);
    }
}