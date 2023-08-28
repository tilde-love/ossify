using System;

namespace Ossify.Bindings
{
    public abstract class GetterSetterBinding<TBound, TValue> : IIHierarchyBinding where TBound : class
    {
        private readonly Func<TValue> getter;

        private readonly Action<TValue> setter;
        public TBound Bound { get; }

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

        /// <inheritdoc />
        public virtual void Dispose() { }

        public void Cache()
        {
            SetValue(Value);
        }

        protected void InvokeValueChanged(TValue value)
        {
            Value = value;
        }

        protected abstract void SetValue(TValue value);
    }
}