using Sirenix.OdinInspector;
using UnityEngine;

namespace Ossify.Variables
{
    public sealed class FloatPublisher : MonoBehaviour
    {
        [SerializeField] private FloatReference variable = new();
        [SerializeField, HideInInspector] private float value;

        [ShowInInspector]
        public float Value
        {
            get => value;
            set
            {
                this.value = value;
                variable.Value = value;
            }
        }

        private void OnEnable()
        {
            variable.Value = value;
        }
    }
}