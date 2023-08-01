using System;

namespace Ossify.Bindings
{
    /// <summary>
    ///     Binds onto a method on the API with no parameters.
    /// </summary>
    /// <typeparam name="TBound"></typeparam>
    public abstract class MethodBinding<TBound> : IIHierarchyBinding where TBound : class
    {
        private readonly Action action;
        public TBound Bound { get; }

        public MethodBinding(TBound bound, Action action)
        {
            Bound = bound ?? throw new ArgumentNullException(nameof(bound));
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        /// <inheritdoc />
        public virtual void Dispose() { }

        /// <summary>
        ///     Does nothing.
        /// </summary>
        public void Cache() { }

        protected void Invoke()
        {
            action?.Invoke();
        }
    }
}