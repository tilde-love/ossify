using Ossify.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace Ossify.Bindings.Specific.Unity
{
    [RequireComponent(typeof(Image))]
    public sealed class FilledImageToFloatBinder : MonoBehaviour
    {
        [SerializeField] private FloatReference variable;

        [SerializeField] private FloatReference minValue = new() { Value = 0 };
        [SerializeField] private FloatReference maxValue = new() { Value = 1 };

        [SerializeField] private FloatReference minFill = new() { Value = 0 };
        [SerializeField] private FloatReference maxFill = new() { Value = 1 };

        private FilledImageBinding binding;
        private Image image;

        private void Awake()
        {
            image = GetComponent<Image>();
            binding = new FilledImageBinding(image, Getter);
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

        private float Getter() =>
            Mathf.Clamp(
                Remap(
                    Mathf.Clamp(variable.Value, minValue.Value, maxValue.Value),
                    minValue.Value,
                    minFill.Value,
                    maxValue.Value,
                    maxFill.Value
                ),
                minFill.Value,
                maxFill.Value
            );

        private void OnValueChanged(float value)
        {
            binding.Cache();
        }

        private static float Remap(
            float value,
            float min,
            float newMin,
            float max,
            float newMax) =>
            newMin + (value - min) * (newMax - newMin) / (max - min);
    }
}