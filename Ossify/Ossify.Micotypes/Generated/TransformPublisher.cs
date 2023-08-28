using UnityEngine;

namespace Ossify.Microtypes
{
    public sealed class TransformPublisher : MonoBehaviour
    {
        [SerializeField] private TransformVariable variable;

        private void OnEnable()
        {
            variable.Value = transform;
        }

        private void OnDisable()
        {
            variable.Value = null;
        }
    }
}