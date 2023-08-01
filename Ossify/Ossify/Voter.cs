using Ossify.Variables;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ossify.Ballots
{
    public sealed class Voter : MonoBehaviour
    {
        [FormerlySerializedAs("activation"), SerializeField]
        private Ballot ballot;

        [SerializeField] private BoolReference value = new() { Value = true };

        private Ballot.Reference reference;

        public bool Value
        {
            get => value.Value;
            set => this.value.Value = value;
        }

        private void OnEnable()
        {
            reference = ballot.GetReference(value.Value);

            value.ValueChanged += OnValueChanged;
        }

        private void OnDisable()
        {
            value.ValueChanged -= OnValueChanged;

            reference.Dispose();
        }

        private void OnValueChanged(bool value) => reference.Active = value;
    }
}