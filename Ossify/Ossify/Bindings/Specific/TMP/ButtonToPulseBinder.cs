using Ossify.Activations;
using UnityEngine;
using UnityEngine.UI;

namespace Ossify.Bindings.Specific.TMP
{
    [RequireComponent(typeof(Button))]
    public sealed class ButtonToPulseBinder : MonoBehaviour
    {
        [SerializeField] private Pulse pulse;
        private Button bound;

        private void Awake() => bound = GetComponent<Button>();

        private void OnEnable() => bound.onClick.AddListener(OnClicked);

        private void OnDisable() => bound.onClick.RemoveListener(OnClicked);

        private void OnClicked() => pulse.Invoke();
    }
}