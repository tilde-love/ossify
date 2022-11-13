using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Ossify
{
    public abstract class Feature : ScriptableObject
    {
        [SerializeField, TextArea] private string comment;

        public abstract UniTask Enable(CancellationToken cancellationToken);

        public abstract void Disable();
    }
}