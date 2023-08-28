using System;

namespace Ossify
{
    public interface IVariable<T> : IVariable
    {
        new T Value { get; set; }

        void SetProtectedValue(T value);

        new event Action<T> ValueChanged;
    }

    public interface IVariable
    {
        VariableAccess Access { get; }

        object Value { get; set; }

        Type ValueType { get; }

        void SetProtectedValue(object value);

        event Action<object> ValueChanged;
    }

    public enum VariableAccess
    {
        Protected,

        Volatile
    }
}