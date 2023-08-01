using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Ossify
{
    public sealed class CallMeOneTime : AsyncMonoBehaviour
    {
        private static CallMeOneTime current;

        private readonly Queue<Action> actionQueue = new();

        private readonly Queue<Call> callQueue = new();

        private void Awake()
        {
            if (current != null) throw new InvalidOperationException("Only one CallMeOneTime can exist at a time.");

            current = this;
        }

        private async void OnEnable()
        {
            CancellationToken cancel = CancellationToken;

            try
            {
                while (cancel.IsCancellationRequested == false)
                {
                    while (callQueue.Count > 0)
                    {
                        Call call = callQueue.Dequeue();
                        call.queued = false;
                        call.Invoke();
                    }

                    while (actionQueue.Count > 0)
                    {
                        Action action = actionQueue.Dequeue();
                        action.Invoke();
                    }

                    await UniTask.NextFrame(PlayerLoopTiming.PreUpdate, cancel);
                }
            }
            catch (Exception e) when (e.IsCancellation()) { }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        private void OnDestroy()
        {
            if (current == this) current = null;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnBeforeSceneLoadRuntimeMethod()
        {
            GameObject newObject = new("Caller");

            CallMeOneTime callMeOneTime = newObject.AddComponent<CallMeOneTime>();

            newObject.hideFlags = HideFlags.DontSave;

            DontDestroyOnLoad(newObject);
        }

        public static void Enqueue(Call call)
        {
            if (call.queued) return;

            current.callQueue.Enqueue(call);

            call.queued = true;
        }

        public static void Enqueue(Action action)
        {
            if (current.actionQueue.Contains(action)) return;

            current.actionQueue.Enqueue(action);
        }

        public static Call Get(Action action) => new(action);

        public class Call
        {
            internal bool queued;

            private readonly Action action;

            public Call(Action action) => this.action = action;

            internal void Invoke() => action?.Invoke();
        }
    }
}