using System;

namespace Ossify.Bindings
{
    /// <summary>
    /// Binds onto a method on the API with no parameters.
    /// </summary>
    /// <typeparam name="TBound"></typeparam>
    public abstract class MethodBinding<TBound> : IIHierarchyBinding where TBound : class 
    {
        public TBound Bound { get; }

        private readonly Action action;

        public MethodBinding(TBound bound, Action action)
        {
            Bound = bound ?? throw new ArgumentNullException(nameof(bound));
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        protected void Invoke() => action?.Invoke();

        /// <inheritdoc />
        public virtual void Dispose() { }

        /// <summary>
        /// Does nothing.
        /// </summary>
        public void Cache() { } 
    }
}