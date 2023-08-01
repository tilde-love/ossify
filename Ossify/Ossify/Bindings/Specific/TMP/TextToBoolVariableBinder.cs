using System;
using Ossify.Variables;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Ossify.Bindings.Specific.TMP
{
    [RequireComponent(typeof(TMP_Text))]
    public sealed class TextToBoolVariableBinder : MonoBehaviour
    {
        [SerializeField] private BoolVariable variable;

        [SerializeField] private StringReference trueString = new() { Value = "True" };
        [SerializeField] private StringReference falseString = new() { Value = "False" };

        [SerializeField] private BoolEvent onValueChanged = new();

        private TMP_Text bound;

        private bool? value;

        private void Awake()
        {
            bound = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            value = null;

            variable.ValueChanged += OnValueChanged;

            OnValueChanged(variable.Value);
        }

        private void OnDisable()
        {
            variable.ValueChanged -= OnValueChanged;
        }

        private void OnValueChanged(bool value)
        {
            if (this.value != null && this.value.Value == value)
            {
                return;
            }

            this.value = value;

            bound.text = value ? trueString.Value : falseString.Value;

            onValueChanged.Invoke(value);
        }

        [Serializable]
        public sealed class BoolEvent : UnityEvent<bool> { }
    }
}