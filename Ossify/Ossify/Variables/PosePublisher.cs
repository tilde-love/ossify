using UnityEngine;
using UnityEngine.Serialization;

namespace Ossify.Variables
{
    public sealed class PosePublisher : MonoBehaviour
    {
        [FormerlySerializedAs("pose"),SerializeField] private PoseVariable variable;

        void Update()
        {
            var t = transform; 
            
            variable.Value = new Pose(t.position, t.rotation);
        }
    }
}