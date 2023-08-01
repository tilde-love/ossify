using UnityEngine;

namespace Ossify.Ossify.Microtypes
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleDispensable : Dispensable
    {
        private ParticleSystem[] systems;

        private void Awake() => systems = GetComponentsInChildren<ParticleSystem>();

        private void Update()
        {
            foreach (var system in systems)
            {
                if (system.isPlaying) return;
            }

            ReturnToDispenser();
        }

        /// <inheritdoc />
        protected override void OnReset()
        {
            base.OnReset();
            foreach (var system in systems)
            {
                system.Play();
            }
        }

        /// <inheritdoc />
        protected override void OnSuspend()
        {
            base.OnSuspend();
            
            foreach (var system in systems)
            {            
                system.Stop(true);
                system.Clear(true);
            }
        }
    }
}