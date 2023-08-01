using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ossify.Variables
{
    public abstract class Publisher<TValue, TVariable, TReference> : MonoBehaviour
        where TVariable : Variable<TValue>
        where TReference : Reference<TValue, TVariable>
    {
        [FormerlySerializedAs("variable"), SerializeField]
        private TReference reference;

        [SerializeField, HideInInspector] private TValue value;

        [ShowInInspector]
        public TValue Value
        {
            get => reference.Value;
            set
            {
                this.value = value;
                reference.Value = value;
            }
        }

        private void OnEnable() => reference.Value = value;

        public void SetValue(TValue value) => Value = value;
    }
}