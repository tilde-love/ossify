using UnityEngine;
using UnityEngine.Serialization;

namespace Ossify.Variables
{
    public sealed class PosePublisher : MonoBehaviour
    {
        [FormerlySerializedAs("pose"), SerializeField]
        private PoseVariable variable;

        private void Update()
        {
            Transform t = transform;

            variable.Value = new Pose(t.position, t.rotation);
        }
    }
}