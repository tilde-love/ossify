using System;

namespace Ossify.Variables
{
    public interface IVariable<T> : IVariable
    {
        new T Value { get; set; }

        new event Action<T> ValueChanged;

        void SetProtectedValue(T value);
    }

    public interface IVariable
    {        
        object Value { get; set; }

        Type ValueType { get; }

        event Action<object> ValueChanged;

        VariableAccess Access { get; } 

        void SetProtectedValue(object value);
    }

    public enum VariableAccess 
    {
        Protected,
        Volatile
    }
}