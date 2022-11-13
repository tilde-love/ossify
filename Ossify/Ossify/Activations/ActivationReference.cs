using System;
using UnityEngine;

namespace Ossify.Activations
{
    public sealed class ActivationReference : MonoBehaviour
    {
        [SerializeField] private Activation activation;

        private IDisposable reference;

        private void OnEnable() => reference = activation.GetReference();

        private void OnDisable() => reference.Dispose();
    }
}