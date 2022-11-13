using Ossify.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace Ossify.Bindings.Specific.Unity
{
    [RequireComponent(typeof(Image))]
    public sealed class ImageToSpriteBinder : MonoBehaviour
    {
        [SerializeField] private SpriteVariable variable;
        private ImageBinding binding;
        private Image image;

        private void Awake()
        {
            image = GetComponent<Image>();
            binding = new ImageBinding(image, Getter);
        }

        private void OnEnable()
        {
            variable.ValueChanged += OnValueChanged;

            OnValueChanged(variable.Value);
        }

        private void OnDisable() => variable.ValueChanged -= OnValueChanged;

        private Sprite Getter() => variable.Value;

        private void OnValueChanged(Sprite value) => binding.Cache();
    }
}