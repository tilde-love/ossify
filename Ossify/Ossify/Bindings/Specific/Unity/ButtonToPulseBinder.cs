using Ossify.Activations;
using Ossify.Bindings.Specific.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Ossify.Bindings.Specific.TMP
{
    [RequireComponent(typeof(Button))]
    public sealed class ButtonToPulseBinder : MonoBehaviour
    {
        [SerializeField] private Pulse pulse;
        private ButtonBinding binding;
        private Button bound;

        private void Awake() => bound = GetComponent<Button>();

        private void OnEnable() => binding = new ButtonBinding(bound, OnClicked);

        private void OnDisable() => binding.Dispose();

        private void OnClicked() => pulse.Invoke();
    }
}