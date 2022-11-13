using UnityEngine;

namespace Ossify.Variables
{
    public sealed class PoseSubscriber : MonoBehaviour
    {
        [SerializeField] private PoseVariable pose;
        
        void Update()
        {
            var t = transform;

            t.position = pose.Value.position;
            t.rotation = pose.Value.rotation;
        }
    }
}