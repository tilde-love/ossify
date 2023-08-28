using UnityEngine;

namespace Ossify
{
    public sealed class Impulser : MonoBehaviour
    {
        [SerializeField] private Impulse onStart;
        [SerializeField] private Impulse onEnable;
        [SerializeField] private Impulse onDisable;        
        [SerializeField] private Impulse onDestroy;
        
        private void Start()
        {
            if (onStart != null) onStart.Invoke();
        }

        private void OnEnable()
        {
            if (onEnable != null) onEnable.Invoke();
        }

        private void OnDisable()
        {
            if (onDisable != null) onDisable.Invoke();
        }

        private void OnDestroy()
        {
            if (onDestroy != null) onDestroy.Invoke();
        }
    }
}