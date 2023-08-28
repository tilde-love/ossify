using UnityEngine;

namespace Ossify.Microtypes
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

            Vector3 originalPosition = t.position;
            Vector3 posePosition = pose.Value.position;

            Vector3 newPosition = new(
                LockPositionX ? originalPosition.x : posePosition.x,
                LockPositionY ? originalPosition.y : posePosition.y,
                LockPositionZ ? originalPosition.z : posePosition.z
            );

            t.position = newPosition;

            Vector3 originalRotation = t.eulerAngles;
            Vector3 poseRotation = pose.Value.rotation.eulerAngles;

            Vector3 newRotation = new(
                LockRotationPitch ? originalRotation.x : poseRotation.x,
                LockRotationYaw ? originalRotation.y : poseRotation.y,
                LockRotationRoll ? originalRotation.z : poseRotation.z
            );

            t.eulerAngles = newRotation;
        }
    }
}