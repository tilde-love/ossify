#if OSSIFY_DOOZYUI
using Doozy.Runtime.UIManager.Components;
using Ossify.Variables;
using UnityEngine;

namespace Ossify.Bindings.Specific.Doozy
{
    [RequireComponent(typeof(UISlider))]
    public sealed class UISliderToFloatVariableBinder : MonoBehaviour
    {
        private UISlider bound;
        
        private UISliderBinding binding;
        
        [SerializeField] FloatReference variable;
        
        void Awake()
        {
            bound = GetComponent<UISlider>();
            binding = new UISliderBinding(bound, Getter, Setter);
        }

        private void Setter(float value) => variable.Value = value;

        private float Getter() => variable.Value;

        private void Start() => OnValueChanged(variable.Value);

        private void OnEnable() => variable.ValueChanged += OnValueChanged;

        private void OnDisable() => variable.ValueChanged -= OnValueChanged;

        private void OnValueChanged(float value) => binding.Cache();
    }
}
#endif