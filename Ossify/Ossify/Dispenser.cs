using System;
using System.Buffers;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Ossify
{
    public abstract class Dispenser<TValue> : CustodianBackbone where TValue : Object
    {
        [SerializeField] private int defaultCapacity = 100;
        [SerializeField] private bool preAllocate = true;

        [SerializeReference, InlineEditor] private Distribution distribution;

        [SerializeReference, InlineEditor] private ScriptableCollection<TValue> items;

        private readonly List<SingleItemDispenser> singleItemDispensers = new();

        private int[] history;
        private int historyIndex;

        [ShowInInspector, ReadOnly]
        public bool CanDispense { get; private set; }

        public TValue Dispense() =>
            CanDispense == false
                ? null
                : singleItemDispensers[distribution.Pick(singleItemDispensers.Count, history, ref historyIndex)]
                    .Dispense();

        /// <inheritdoc />
        protected override void Begin()
        {
            if (items == null) return;

            if (items.Count == 0) return;

            if (distribution == null) distribution = CreateInstance<RandomDistribution>();

            history = ArrayPool<int>.Shared.Rent(distribution.HistorySize);

            foreach (TValue item in items)
            {
                singleItemDispensers.Add(new SingleItemDispenser(item, defaultCapacity, preAllocate));
            }

            CanDispense = true;
        }

        /// <inheritdoc />
        protected override void End()
        {
            CanDispense = false;

            foreach (SingleItemDispenser singleItemDispenser in singleItemDispensers)
            {
                singleItemDispenser.Dispose();
            }

            singleItemDispensers.Clear();

            if (history == null) return;

            ArrayPool<int>.Shared.Return(history);

            history = null;
        }

        private sealed class SingleItemDispenser : IDisposable
        {
            private readonly ObjectPool<Instance> pool;

            private readonly TValue prefab;

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

                ListPool<Instance>.Get(out List<Instance> list);

                for (int i = 0; i < defaultCapacity; i++)
                {
                    list.Add(pool.Get());
                }

                list.ForEach(pool.Release);

                ListPool<Instance>.Release(list);
            }

            /// <inheritdoc />
            public void Dispose() => pool.Dispose();

            public TValue Dispense()
            {
                Instance item = pool.Get();

                return item.Value;
            }

            private void ActionOnDestroy(Instance instance) => instance.Destroy();

            private void ActionOnGet(Instance instance) => instance.Reset();

            private void ActionOnRelease(Instance instance) => instance.Suspend();

            private Instance Create() => new(Instantiate(prefab), ReturnToDispenser);

            private void ReturnToDispenser(Instance instance) => pool.Release(instance);

            private class Instance
            {
                public readonly TValue Value;

                private readonly IDispensedHandle dispensedHandle;

                private readonly Action<Instance> returnToDispenser;

                public Instance(TValue value, Action<Instance> returnToDispenser)
                {
                    Value = value;

                    GameObject gameObject = value switch
                    {
                        GameObject go => go,
                        Component component => component.gameObject,
                        _ => throw new Exception($"Value must be a GameObject or Component, but was {value.GetType().Name}.")
                    };

                    this.returnToDispenser = returnToDispenser;

                    IDispensable dispensable = gameObject.GetComponent<IDispensable>();

                    if (dispensable == null) dispensable = new DefaultDispensable(gameObject);

                    dispensedHandle = dispensable.Initialize(ReturnMeToDispenser);
                }

                public void Destroy() => dispensedHandle.Destroy();

                public void Reset() => dispensedHandle.Reset();

                public void Suspend() => dispensedHandle.Suspend();

                private void ReturnMeToDispenser() => returnToDispenser(this);
            }
        }
    }

    public interface IDispensedHandle
    {
        void Destroy();

        void Reset();

        void Suspend();
    }

    public interface IDispensable
    {
        IDispensedHandle Initialize(Action returnMeToDispenser);

        void ReturnToDispenser();
    }

    public class DefaultDispensable : IDispensable
    {
        private readonly IDispensedHandle dispensedHandle;

        private readonly GameObject gameObject;

        private Action returnMeToDispenser;

        public DefaultDispensable(GameObject gameObject)
        {
            dispensedHandle = new DispensedHandle(gameObject);

            this.gameObject = gameObject;
        }

        public IDispensedHandle Initialize(Action returnMeToDispenser)
        {
            if (this.returnMeToDispenser != null)
            {
                throw new Exception("Dispensable has already been initialized.");
            }

            this.returnMeToDispenser = returnMeToDispenser;

            return dispensedHandle;
        }

        public void ReturnToDispenser() => returnMeToDispenser?.Invoke();

        private class DispensedHandle : IDispensedHandle
        {
            private readonly GameObject gameObject;

            public DispensedHandle(GameObject gameObject) => this.gameObject = gameObject;

            public void Destroy() => Object.Destroy(gameObject);

            public void Reset() => gameObject.SetActive(true);

            public void Suspend() => gameObject.SetActive(false);
        }
    }
}