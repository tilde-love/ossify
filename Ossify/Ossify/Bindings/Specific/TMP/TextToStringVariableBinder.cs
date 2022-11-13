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
        TMP_Text bound;
        
        [SerializeField] StringVariable variable;
        
        [SerializeField] StringEvent onValueChanged = new ();
        
        void Awake() => bound = GetComponent<TMP_Text>();

        private void OnEnable()
        {
            variable.ValueChanged += OnValueChanged;
            
            OnValueChanged(variable.Value);
        }
        
        private void OnDisable() => variable.ValueChanged -= OnValueChanged;

        private void OnValueChanged(string value)
        {
            if (string.Equals(bound.text, value, StringComparison.OrdinalIgnoreCase)) return;
            
            bound.gameObject.SetActive(false);
            
            bound.text = value;
            
            bound.gameObject.SetActive(true);
            
            bound.ForceMeshUpdate(true);
            
            onValueChanged.Invoke(value);
        }
    }
    
    [Serializable] public sealed class StringEvent : UnityEvent<string> { }
}