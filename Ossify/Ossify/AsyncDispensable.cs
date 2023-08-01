using System;
using Sirenix.OdinInspector;

namespace Ossify
{
    public class AsyncDispensable : AsyncMonoBehaviour, IDispensable, IDispensedHandle
    {
        private Action returnMeToDispenser;

        /// <inheritdoc />
        public IDispensedHandle Initialize(Action returnMeToDispenser)
        {
            this.returnMeToDispenser = returnMeToDispenser;
            
            OnInitialize();
 
            return this;
        }

        [Button] public void ReturnToDispenser() => returnMeToDispenser?.Invoke();

        void IDispensedHandle.Reset()
        {
            if (this != null && gameObject != null) OnReset();
        }

        void IDispensedHandle.Suspend()
        {
            if (this != null && gameObject != null) OnSuspend();
        }

        void IDispensedHandle.Destroy()
        {
            if (this != null && gameObject != null) OnDestroy();
        }

        protected virtual void OnInitialize() { }
        protected virtual void OnReset() => gameObject.SetActive(true);
        protected virtual void OnSuspend() => gameObject.SetActive(false);
        protected virtual void OnDestroy() => Destroy(gameObject);
    }
}