using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Ossify.Ossify.Microtypes
{
    public class FusedDispensable : MonoBehaviour, IDispensable, IDispensedHandle
    {
        [SerializeField] private float duration = 4;
        private Action returnMeToDispenser;
        private float time;

        private void Update()
        {
            if (time >= duration)
            {
                ReturnToDispenser();
            }
            else
            {
                time += Time.deltaTime;
            }
        }

        /// <inheritdoc />
        public IDispensedHandle Initialize(Action returnMeToDispenser)
        {
            this.returnMeToDispenser = returnMeToDispenser;

            return this;
        }

        [Button]
        public void ReturnToDispenser()
        {
            returnMeToDispenser?.Invoke();
        }

        void IDispensedHandle.Reset()
        {
            time = 0;
            gameObject.SetActive(true);
        }

        void IDispensedHandle.Suspend()
        {
            gameObject.SetActive(false);
        }

        void IDispensedHandle.Destroy()
        {
            if (this != null && gameObject != null)
            {
                Destroy(gameObject);
            }
        }
    }
}