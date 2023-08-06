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

        private int currentQueue = 0;

        private readonly Queue<Action>[] actionQueue = { new (), new () };

        private readonly Queue<Call>[] callQueue = { new (), new () };

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
                    var calls = callQueue[currentQueue];
                    var actions = actionQueue[currentQueue];

                    currentQueue = (currentQueue + 1) % 2;
 
                    while (calls.Count > 0)
                    {
                        Call call = calls.Dequeue();
                        call.queued = false;
                        call.Invoke();
                    }

                    while (actions.Count > 0)
                    {
                        Action action = actions.Dequeue();
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

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void OnBeforeSceneLoadRuntimeMethod()
        {
            if (current != null) return;

            GameObject newObject = new("Caller");

            CallMeOneTime callMeOneTime = newObject.AddComponent<CallMeOneTime>();

            newObject.hideFlags = HideFlags.DontSave;

            DontDestroyOnLoad(newObject);
        }

        public static void Enqueue(Call call)
        {
            if (call.queued) return;

            current.callQueue[current.currentQueue].Enqueue(call);

            call.queued = true;
        }

        public static void Enqueue(Action action)
        {
            if (current.actionQueue[current.currentQueue].Contains(action)) return;

            current.actionQueue[current.currentQueue].Enqueue(action);
        }

        public static Call Get(Action action) => new(action);

        public class Call
        {
            internal bool queued;

            private readonly Action action;

            public Call(Action action) => this.action = action;

            internal void Invoke() =>  action?.Invoke();
        }
    }
}