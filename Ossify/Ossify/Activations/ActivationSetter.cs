using Sirenix.OdinInspector;
using UnityEngine;

namespace Ossify.Activations
{
    public sealed class ActivationSetter : MonoBehaviour
    {
        public enum Mode
        {
            Enable,
            Disable,
            Toggle
        }

        [SerializeField] private ActivationSet set;

        [SerializeField] private Activation activation;

        [SerializeField] private Mode mode;

        [SerializeField, DisableIf("@mode == Mode.Toggle")]
        private bool onEnableOnly;

        private void OnEnable()
        {
            switch (mode)
            {
                case Mode.Toggle:
                case Mode.Enable:
                    set[activation] = true;
                    break;
                case Mode.Disable:
                    set[activation] = false;
                    break;
            }
        }

        private void OnDisable()
        {
            if (mode != Mode.Toggle && onEnableOnly)
            {
                return;
            }

            switch (mode)
            {
                case Mode.Toggle:
                case Mode.Enable:
                    set[activation] = false;
                    break;
                case Mode.Disable:
                    set[activation] = true;
                    break;
            }
        }
    }
}