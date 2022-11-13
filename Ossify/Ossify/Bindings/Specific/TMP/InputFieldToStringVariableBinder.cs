using Ossify.Variables;
using TMPro;
using UnityEngine;

namespace Ossify.Bindings.Specific.TMP
{
    [RequireComponent(typeof(TMP_InputField))]
    public sealed class InputFieldToStringVariableBinder : MonoBehaviour
    {
        [SerializeField] private StringReference variable;

        private InputFieldBinding binding;
        private TMP_InputField bound;

        private void Awake()
        {
            bound = GetComponent<TMP_InputField>();
            binding = new InputFieldBinding(bound, Getter, Setter);
        }

        private void Start() => OnValueChanged(variable.Value);

        private void OnEnable() => variable.ValueChanged += OnValueChanged;

        private void OnDisable() => variable.ValueChanged -= OnValueChanged;

        private void Setter(string value) => variable.Value = value;

        private string Getter() => variable.Value;

        private void OnValueChanged(string value) => binding.Cache();
    }
}