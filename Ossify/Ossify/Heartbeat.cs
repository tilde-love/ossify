using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ossify
{
    public class Heartbeat : AsyncMonoBehaviour
    {
        [SerializeReference, InlineEditor, InlineProperty] private Backbone backbone;
        
        private CustodianBackbone.Artifact running;

        async void OnEnable()
        {
            if (backbone == null) return;

            if (backbone is CustodianBackbone bookendBackbone)
            {
                running = bookendBackbone.Get();

                return;
            }
            
            if (backbone is not AsyncBackbone asyncBackbone) 
            {
                throw new Exception($"Backbone {backbone} is not a known type of Backbone.");
            }

            try
            {
                await asyncBackbone.PulseAsync(CancellationToken);
            }
            catch (Exception ex) when (ex.IsCancellation())
            {
            }
            catch (Exception ex) 
            {
                Debug.LogException(ex);
            }
        }

        /// <inheritdoc />
        protected override void OnDisable()
        {
            base.OnDisable();

            running?.Dispose();
            running = null;
        }
    }
}