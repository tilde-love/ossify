using System.Collections.Generic;
using UnityEngine;

namespace Ossify.Activations
{
    [CreateAssetMenu(order = Consts.ActivationMenuItems, menuName = "Variables/Activation Set")]
    public sealed class ActivationSet : ScriptableObject
    {
        [SerializeField] private List<Activation> activations = new();
        private readonly Dictionary<Activation, Activation.Reference> references = new();

        public bool this[Activation activation]
        {
            get => references[activation].Active;
            set => references[activation].Active = value;
        }

        private void OnEnable()
        {
            foreach (Activation activation in activations)
            {
                references[activation] = activation.GetReference(false);
            }
        }

        private void OnDisable()
        {
            foreach (KeyValuePair<Activation, Activation.Reference> keyValuePair in references)
            {
                keyValuePair.Value.Dispose();
            }

            references.Clear();
        }
    }
}