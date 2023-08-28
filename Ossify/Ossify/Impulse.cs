using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Ossify
{
    [CreateAssetMenu(order = Consts.ActivationMenuItems, menuName = "Variables/Impulse")]
    public sealed class Impulse : ScriptableObject
    {
        public event Action Pulsed;

        [Button]
        public void Invoke() => Pulsed?.Invoke();

        public async UniTask Wait(CancellationToken cancellationToken)
        {
            bool pulsed = false;

            void OnPulsed()
            {
                pulsed = true;
            }

            Pulsed += OnPulsed;

            try
            {
                await UniTask.WaitUntil(() => pulsed, cancellationToken: cancellationToken);
            }
            finally
            {
                Pulsed -= OnPulsed;
            }
        }
    }    
}