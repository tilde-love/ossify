#if OSSIFY_DOOZYUI
using Doozy.Runtime.UIManager.Components;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ossify.Bindings.Specific.Doozy
{
    [RequireComponent(typeof(UIButton))]
    public sealed class UIButtonToPulseBinder : MonoBehaviour
    {
        [FormerlySerializedAs("pulse"), SerializeField]
        private Impulse impulse;

        private UIButtonBinding binding;
        private UIButton bound;

        private void Awake()
        {
            bound = GetComponent<UIButton>();
        }

        private void OnEnable()
        {
            binding = new UIButtonBinding(bound, OnClicked);
        }

        private void OnDisable()
        {
            binding.Dispose();
        }

        private void OnClicked()
        {
            impulse.Invoke();
        }
    }
}
#endif