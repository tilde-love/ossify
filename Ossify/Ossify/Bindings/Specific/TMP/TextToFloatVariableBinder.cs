using System;
using Ossify.Variables;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Ossify.Bindings.Specific.TMP
{
    [RequireComponent(typeof(TMP_Text))]
    public sealed class TextToFloatVariableBinder : MonoBehaviour
    {
        [SerializeField] private FloatVariable variable;
        
        private float? value;
        
        [SerializeField] private StringReference format = new () { Value = "{0}" };

        [SerializeField] private FloatEvent onValueChanged = new();
        
        private TMP_Text bound;

        private void Awake() => bound = GetComponent<TMP_Text>();

        private void OnEnable()
        {
            value = null; 
            
            variable.ValueChanged += OnValueChanged;

            OnValueChanged(variable.Value);
        }

        private void OnDisable() => variable.ValueChanged -= OnValueChanged;

        private void OnValueChanged(float value)
        {
            if (this.value != null && Mathf.Abs(this.value.Value - value) < float.Epsilon)
            {
                return;
            }

            this.value = value;

            bound.gameObject.SetActive(false);

            bound.text = string.Format(format.Value, value);

            bound.gameObject.SetActive(true);

            bound.ForceMeshUpdate(true);

            onValueChanged.Invoke(value);
        }
        
        [Serializable]
        public sealed class FloatEvent : UnityEvent<float>
        {
        }
    }
}