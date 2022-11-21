#if OSSIFY_DOOZYUI
using Doozy.Runtime.UIManager.Components;
using Ossify.Activations;
using UnityEngine;

namespace Ossify.Bindings.Specific.Doozy
{
    [RequireComponent(typeof(UIButton))]
    public sealed class UIButtonToPulseBinder : MonoBehaviour
    {
        [SerializeField] private Pulse pulse;
        private UIButtonBinding binding;
        private UIButton bound;

        private void Awake() => bound = GetComponent<UIButton>();

        private void OnEnable() => binding = new UIButtonBinding(bound, OnClicked);

        private void OnDisable() => binding.Dispose();

        private void OnClicked() => pulse.Invoke();
    }
}
#endif