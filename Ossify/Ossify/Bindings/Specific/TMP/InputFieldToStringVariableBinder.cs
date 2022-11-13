using Ossify.Variables;
using TMPro;
using UnityEngine;

namespace Ossify.Bindings.Specific.TMP
{
    [RequireComponent(typeof(TMP_InputField))]
    public sealed class InputFieldToStringVariableBinder : MonoBehaviour
    {
        private TMP_InputField bound;
        
        private InputFieldBinding binding;
        
        [SerializeField] StringReference variable;
        
        void Awake()
        {
            bound = GetComponent<TMP_InputField>();
            binding = new InputFieldBinding(bound, Getter, Setter);
        }

        private void Setter(string value) => variable.Value = value;

        private string Getter() => variable.Value;

        private void Start() => OnValueChanged(variable.Value);

        private void OnEnable() => variable.ValueChanged += OnValueChanged;

        private void OnDisable() => variable.ValueChanged -= OnValueChanged;

        private void OnValueChanged(string value) => binding.Cache();
    }
}