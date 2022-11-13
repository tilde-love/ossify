using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Ossify.Variables
{
    public abstract class RuntimeVariable<T> : ScriptableObject, IVariable<T> where T : class 
    {
        [SerializeField, TextArea] private string comment;
        
        private T value; 
        
        public event Action<T> ValueChanged;
        
        [ShowInInspector, OnValueChanged(nameof(OnEditorValueChanged))] 
        public T Value
        {
            get => value;
            set
            {
                this.value = value;
                
                ValueChanged?.Invoke(value); 
            }
        }

        private void OnEditorValueChanged()
        {
            if (Application.isPlaying)
            {
                ValueChanged?.Invoke(value);
            }
        }
        
        public async UniTask<T> WaitForValue(CancellationToken cancellationToken)
        {
            if (Value != null) return Value;
            
            await UniTask.WaitUntil(() => Value != null, cancellationToken: cancellationToken);

            return Value;
        }
    }
}