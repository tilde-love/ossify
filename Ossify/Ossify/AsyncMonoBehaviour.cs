using System.Threading;
using UnityEngine;

namespace Ossify
{
    public abstract class AsyncMonoBehaviour : MonoBehaviour
    {
        private CancellationTokenSource disabledTokenSource;

        protected CancellationToken CancellationToken => (disabledTokenSource ??= new CancellationTokenSource()).Token;

        protected virtual void OnDisable()
        {
            disabledTokenSource?.Cancel();
            disabledTokenSource?.Dispose();
            disabledTokenSource = null;
        }
    }
}