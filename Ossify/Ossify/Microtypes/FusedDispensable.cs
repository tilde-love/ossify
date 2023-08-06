using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Ossify.Ossify.Microtypes
{
    public class FusedDispensable : Dispensable
    {
        [SerializeField] private float duration = 4;

        private float time;

        private void Update()
        {
            if (time >= duration) ReturnToDispenser();
            else time += Time.deltaTime;
        }

        /// <inheritdoc />
        protected override void OnReset()
        {
            base.OnReset();
            time = 0;
        }
    }
}