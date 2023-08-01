using Ossify.Variables;
using UnityEngine;

namespace Ossify.Bindings.Specific.Unity
{
    [RequireComponent(typeof(Transform))]
    public sealed class LocalScaleToFloatBinder : MonoBehaviour
    {
        [SerializeField] private FloatVariable variable;
        private LocalScaleBinding binding;

        private void Awake()
        {
            binding = new LocalScaleBinding(transform, Getter);
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

        private float Getter() => variable.Value;

        private void OnValueChanged(float value)
        {
            binding.Cache();
        }
    }
}