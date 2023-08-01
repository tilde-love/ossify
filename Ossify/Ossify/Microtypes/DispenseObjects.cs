using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Ossify.Ossify.Microtypes
{
    public class DispenseObjects: AsyncMonoBehaviour
    {
        public ObjectDispenser dispenser;

        [SerializeField] float intervalSeconds = 0.1f;

        async void OnEnable()
        {
            var cancel = CancellationToken; 

            try 
            {
                while (cancel.IsCancellationRequested == false) 
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(intervalSeconds), cancellationToken: cancel);

                    var dispensed = dispenser.Dispense();

                    if (dispensed == null) continue;

                    dispensed.gameObject.transform.position = transform.position;    

                    if (dispensed.GetComponent<Rigidbody>() is { } rigidbody) rigidbody.velocity = Vector3.zero;            
                }
            }
            catch (Exception ex) when (ex.IsCancellation())
            {
            }
            catch (Exception ex) 
            {
                Debug.LogException(ex);
            }
        }            
    }
}