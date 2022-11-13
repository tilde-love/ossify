using System;

namespace Ossify.Bindings
{
    public abstract class GetterBinding<TBound, TValue> : IIHierarchyBinding where TBound : class 
    {
        private readonly Func<TValue> getter;
        
        public TBound Bound { get; }

        public GetterBinding(TBound bound, Func<TValue> getter)
        {
            Bound = bound ?? throw new ArgumentNullException(nameof(bound));
            this.getter = getter ?? throw new ArgumentNullException(nameof(getter));
        }

        public virtual void Dispose() { }

        public void Cache()
        {
            if (Bound == null) return;
            
            SetValue(getter());
        }

        protected abstract void SetValue(TValue value);
    }
}