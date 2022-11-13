using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Ossify.Activations;
using UnityEngine;

namespace Ossify
{
    [DefaultExecutionOrder(-10000)]
    public sealed class Features : MonoBehaviour
    {
        [SerializeField] private Activation activation;

        [SerializeField] private Feature[] features;
        private CancellationTokenSource cancelOnDisable;
        private CancellationTokenSource CancelOnDisable => cancelOnDisable ??= new CancellationTokenSource();

        private async void OnEnable()
        {
            CancellationTokenSource cancel = CancelOnDisable; // local copy

            try
            {
                if (activation == null)
                {
                    await EnableFeatures(cancel.Token);

                    return;
                }

                while (cancel.IsCancellationRequested == false)
                {
                    using Activation.Listener activationListener = activation.GetListener();

                    while (activationListener.IsExpired == false && cancel.IsCancellationRequested == false)
                    {
                        if (activationListener.Active == false)
                        {
                            await UniTask.WaitUntil(() => activationListener.Active, cancellationToken: cancel.Token);

                            continue;
                        }

                        using CancellationTokenSource linked = CancellationTokenSource.CreateLinkedTokenSource(cancel.Token);

                        void OnExpire(Activation.Listener listener)
                        {
                            linked.Cancel();
                        }

                        try
                        {
                            activationListener.Changed += linked.Cancel;
                            activationListener.Expired += OnExpire;

                            await EnableFeatures(linked.Token);
                        }
                        catch (Exception ex) when (ex.IsCancellation())
                        {
                        }
                        finally
                        {
                            activationListener.Expired -= OnExpire;
                            activationListener.Changed -= linked.Cancel;
                        }
                    }
                }
            }
            catch (Exception ex) when (ex.IsCancellation())
            {
                // this is fine
            }
        }

        private void OnDisable()
        {
            cancelOnDisable?.Cancel();
            cancelOnDisable?.Dispose();
            cancelOnDisable = null;
        }

        private async UniTask EnableFeatures(CancellationToken token)
        {
            try
            {
                await UniTask.WhenAll(features.Select(f => f.Enable(token)));

                await UniTask.WaitUntilCanceled(token);
            }
            finally
            {
                List<Exception> aggregateExceptions = null;

                foreach (Feature feature in features)
                {
                    try
                    {
                        if (feature != null)
                        {
                            feature.Disable();
                        }
                    }
                    catch (Exception ex)
                    {
                        (aggregateExceptions ??= new List<Exception>()).Add(ex);
                    }
                }

                if (aggregateExceptions != null)
                {
                    throw new AggregateException(aggregateExceptions);
                }
            }
        }
    }
}