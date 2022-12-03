using UnityEngine;

namespace Ossify.Variables
{
    public sealed class PoseSubscriber : MonoBehaviour
    {
        [SerializeField] private PoseVariable pose;

        public bool LockPositionX;
        public bool LockPositionY;
        public bool LockPositionZ;
        
        public bool LockRotationPitch;
        public bool LockRotationRoll;
        public bool LockRotationYaw;

        private void Update()
        {
            Transform t = transform;

            var originalPosition = t.position;
            var posePosition = pose.Value.position;
            
            var newPosition = new Vector3(
                LockPositionX ? originalPosition.x : posePosition.x,
                LockPositionY ? originalPosition.y : posePosition.y,
                LockPositionZ ? originalPosition.z : posePosition.z
            );
            
            t.position = newPosition;
            
            var originalRotation = t.eulerAngles;
            var poseRotation = pose.Value.rotation.eulerAngles;
            
            var newRotation = new Vector3(
                LockRotationPitch ? originalRotation.x : poseRotation.x,
                LockRotationYaw ? originalRotation.y : poseRotation.y,
                LockRotationRoll ? originalRotation.z : poseRotation.z
            );

            t.eulerAngles = newRotation;
        }
    }
}