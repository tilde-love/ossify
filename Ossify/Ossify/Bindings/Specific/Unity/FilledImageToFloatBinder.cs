using Ossify.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace Ossify.Bindings.Specific.Unity
{
    [RequireComponent(typeof(Image))]
    public sealed class FilledImageToFloatBinder : MonoBehaviour
    {
        [SerializeField] private FloatVariable variable;
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

        private void OnDisable() => variable.ValueChanged -= OnValueChanged;

        private float Getter() => variable.Value;

        private void OnValueChanged(float value) => binding.Cache();
    }
}