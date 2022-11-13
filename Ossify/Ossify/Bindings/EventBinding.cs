using System;

namespace Ossify.Bindings
{
    public abstract class EventBinding<TBound, TValue> : IIHierarchyBinding where TBound : class 
    {
        public TBound Bound { get; }

        public EventBinding(TBound bound)
        {
            Bound = bound ?? throw new ArgumentNullException(nameof(bound));
        }

        public virtual void Dispose() { }

        /// <summary>
        /// Does nothing.
        /// </summary>
        public void Cache() { }

        public abstract void Raise(TValue value);
    }

    public abstract class EventBinding<TBound> : IIHierarchyBinding where TBound : class
    {
        public TBound Bound { get; }

        public EventBinding(TBound bound)
        {
            Bound = bound ?? throw new ArgumentNullException(nameof(bound));
        }

        /// <summary>
        /// Does nothing.
        /// </summary>
        public void Cache() { }
        
        public virtual void Dispose() { }

        public abstract void Raise();
    }
}