using System;

namespace Ossify.Bindings
{
    public abstract class GetterSetterBinding<TBound, TValue> : IIHierarchyBinding where TBound : class 
    {
        public TBound Bound { get; }

        private readonly Action<TValue> setter;
        private readonly Func<TValue> getter;
        
        public TValue Value
        {
            get => getter();
            set => setter(value);
        }

        public GetterSetterBinding(TBound bound, Func<TValue> getter, Action<TValue> setter)
        {
            Bound = bound ?? throw new ArgumentNullException(nameof(bound));
            this.getter = getter ?? throw new ArgumentNullException(nameof(getter));
            this.setter = setter ?? throw new ArgumentNullException(nameof(setter));
        }

        public void Cache() => SetValue(Value);

        protected abstract void SetValue(TValue value);

        protected void InvokeValueChanged(TValue value) => Value = value;

        /// <inheritdoc />
        public virtual void Dispose() { } 
    }
}