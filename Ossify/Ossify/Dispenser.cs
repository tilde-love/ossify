using System;
using System.Buffers;
using System.Collections.Generic;
using System.Threading;
using Codice.Client.GameUI.Checkin;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Ossify
{
    public abstract class Dispenser<TValue> : CustodianBackbone where TValue : Object 
    {
        private sealed class SingleItemDispenser : IDisposable 
        {
            private class Instance : IDisposable
            {
                public readonly TValue Value; 
                
                public bool Disposed { get; private set; }                

                private readonly IDispensedHandle dispensedHandle;

                private readonly Action<Instance> returnToDispenser;

                public Instance(TValue value, Action<Instance> returnToDispenser)
                {                    
                    Value = value;

                    GameObject thisGameObject = null;

                    if (value is GameObject gameObject) thisGameObject = gameObject;
                    else if (value is Component component) thisGameObject = component.gameObject;
                    else throw new Exception($"Value must be a GameObject or Component, but was {value.GetType().Name}.");    
            
                    this.returnToDispenser = returnToDispenser;

                    IDispensable dispensable = thisGameObject.GetComponent<IDispensable>();

                    if (dispensable == null) dispensable = new DefaultDispensable(thisGameObject);         

                    dispensedHandle = dispensable.Initialize(ReturnMeToDispenser);      
                }

                private void ReturnMeToDispenser()
                {
                    if (Disposed == false) returnToDispenser(this);
                }

                public void Reset()
                {
                    if (Disposed == false) dispensedHandle.Reset();
                }

                public void Suspend()
                {
                    if (Disposed == false) dispensedHandle.Suspend();
                }

                public void Destroy()
                {
                    if (Disposed == false) dispensedHandle.Destroy();
                }

                public void Dispose() { } // => Disposed = true;
            }

            private readonly TValue prefab;  
          
            private readonly ObjectPool<Instance> pool;
            
            public SingleItemDispenser(TValue prefab, int defaultCapacity = 100, bool preAllocate = true)
            {
                this.prefab = prefab;        
         
                pool = new ObjectPool<Instance>(
                    Create,
                    ActionOnGet,
                    ActionOnRelease,
                    ActionOnDestroy,
                    defaultCapacity: defaultCapacity);

                if (preAllocate == false) return;

                ListPool<Instance>.Get(out var list);

                for (var i = 0; i < defaultCapacity; i++) list.Add(pool.Get());

                list.ForEach(pool.Release);

                ListPool<Instance>.Release(list);
            }

            private Instance Create() => new(Instantiate(prefab), ReturnToDispenser);

            private void ActionOnGet(Instance instance) => instance.Reset();

            private void ActionOnRelease(Instance instance) => instance.Suspend();

            private void ActionOnDestroy(Instance instance) => instance.Destroy();

            private void ReturnToDispenser(Instance instance) => pool.Release(instance);

            /// <inheritdoc />
            public void Dispose()
            {
                pool.Dispose();
            }

            public TValue Dispense()
            {
                var item = pool.Get(); 
                
                return item.Value; 
            }
        }

        [ShowInInspector, ReadOnly]
        public bool CanDispense { get; private set; }

        [SerializeField] private int defaultCapacity = 100; 
        [SerializeField] private bool preAllocate = true;


        [SerializeReference, InlineEditor] private Distribution distribution;

        [SerializeReference, InlineEditor] private ScriptableCollection<TValue> items;

        private List<SingleItemDispenser> singleItemDispensers = new ();

        private int[] history;
        private int historyIndex;

        public TValue Dispense()
        {
            if (CanDispense == false) return null;

            return singleItemDispensers[distribution.Pick(singleItemDispensers.Count,  history, ref historyIndex)].Dispense();
        }

        /// <inheritdoc />
        protected override void Begin()
        {
            if (items == null) return;

            if (items.Count == 0) return;

            if (distribution == null) distribution = CreateInstance<RandomDistribution>();

            history = ArrayPool<int>.Shared.Rent(distribution.HistorySize);

            foreach (var item in items)
            {
                singleItemDispensers.Add(new SingleItemDispenser(item, defaultCapacity, preAllocate));
            }

            CanDispense = true;
        }

        /// <inheritdoc />
        protected override void End()
        {
            CanDispense = false; 

            foreach (SingleItemDispenser singleItemDispenser in singleItemDispensers) singleItemDispenser.Dispose();

            singleItemDispensers.Clear();       

            if (history != null) 
            {
                ArrayPool<int>.Shared.Return(history);
                history = null;
            }
        }
    }

    public interface IDispensedHandle
    {
        void Reset();

        void Suspend();

        void Destroy();
    }

    public interface IDispensable 
    {
        IDispensedHandle Initialize(Action returnMeToDispenser);
        
        void ReturnToDispenser();
    }

    public class DefaultDispensable : IDispensable
    {
        private class DispensedHandle : IDispensedHandle
        {
            private readonly GameObject gameObject;

            public DispensedHandle(GameObject gameObject) => this.gameObject = gameObject;

            public void Reset() => gameObject.SetActive(true);

            public void Suspend() => gameObject.SetActive(false);

            public void Destroy() => Object.Destroy(gameObject);
        }

        private readonly GameObject gameObject;
        private readonly IDispensedHandle dispensedHandle;
        private Action returnMeToDispenser; 

        public DefaultDispensable(GameObject gameObject)
        {
            dispensedHandle = new DispensedHandle(gameObject);
            this.gameObject = gameObject;
        }

        public IDispensedHandle Initialize(Action returnMeToDispenser)
        {
            if (this.returnMeToDispenser != null) throw new Exception("Dispensable has already been initialized.");
            this.returnMeToDispenser = returnMeToDispenser;  
            return dispensedHandle;
        }

        public void ReturnToDispenser()
        {
            throw new NotImplementedException();
        }
    }
}