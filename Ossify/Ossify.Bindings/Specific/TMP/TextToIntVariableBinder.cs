using System;
using Ossify.Microtypes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Ossify.Bindings.Specific.TMP
{
    [RequireComponent(typeof(TMP_Text))]
    public sealed class TextToIntVariableBinder : MonoBehaviour
    {
        [SerializeField] private IntVariable variable;

        [SerializeField] private StringReference format = new() { Value = "{0}" };

        [SerializeField] private IntEvent onValueChanged = new();

        private TMP_Text bound;

        private int? value;

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

        private void OnValueChanged(int value)
        {
            if (this.value != null && this.value.Value == value)
            {
                return;
            }

            this.value = value;

            bound.text = string.Format(format.Value, value);

            onValueChanged.Invoke(value);
        }

        [Serializable]
        public sealed class IntEvent : UnityEvent<int> { }
    }
}