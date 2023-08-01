using System;
using Ossify.Variables;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Ossify.Bindings.Specific.TMP
{
    [RequireComponent(typeof(TMP_Text))]
    public sealed class TextToStringVariableBinder : MonoBehaviour
    {
        [SerializeField] private StringVariable variable;

        [SerializeField] private StringEvent onValueChanged = new();
        private TMP_Text bound;

        private void Awake()
        {
            bound = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            variable.ValueChanged += OnValueChanged;

            OnValueChanged(variable.Value);
        }

        private void OnDisable()
        {
            variable.ValueChanged -= OnValueChanged;
        }

        private void OnValueChanged(string value)
        {
            if (string.Equals(bound.text, value, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            bound.text = value;

            onValueChanged.Invoke(value);
        }
    }

    [Serializable]
    public sealed class StringEvent : UnityEvent<string> { }
}