using UnityEngine;

namespace Ossify
{
    [CreateAssetMenu(order = Consts.ActivationMenuItems, menuName = "Variables/Uniform Distribution")]
    public sealed class UniformDistribution : SampleDistribution
    {        
        /// <inheritdoc />
        public override SampleHistory CreateHistory() => new (0);

        /// <inheritdoc />
        public override int Distribute(int input, SampleHistory history) 
        {
            var sample = Random.Range(0, input);
            history.AddSample(sample); 
            return sample;
        }
    }
}