using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Ossify
{
    public static class UniTaskExt
    {
        public static bool IsCancellation(this Exception ex)
        {
            switch (ex)
            {
                case AggregateException ax:
                    // if (ax.InnerExceptions.Count == 1
                    //        && ax.InnerExceptions[0] is OperationCanceledException) return true;
                    //
                    foreach (Exception inner in ax.InnerExceptions)
                    {
                        if (inner.IsCancellation() == false) return false;
                    }
                    
                    return true;
                case OperationCanceledException canceledException:
                    return true;
                default:
                    return false;
            }
        }
        
        public static CancellationTokenSource CreateLinkedTokenSourceOnDestroy(
            this GameObject gameObject, 
            CancellationToken cancellationToken, 
            out CancellationToken linked)
        {
            var result = CancellationTokenSource.CreateLinkedTokenSource(
                gameObject.GetCancellationTokenOnDestroy(),
                cancellationToken
            );
            
            linked = result.Token;
            
            return result;
        }
        
        public static CancellationTokenSource CreateLinkedTokenSourceOnDestroy(
            this Component component, 
            CancellationToken cancellationToken, 
            out CancellationToken linked)
        {
            var result = CancellationTokenSource.CreateLinkedTokenSource(
                component.GetCancellationTokenOnDestroy(),
                cancellationToken
            );
            
            linked = result.Token;
            
            return result;
        }
    }
}