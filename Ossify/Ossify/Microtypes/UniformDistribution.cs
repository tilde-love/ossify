using UnityEngine;

namespace Ossify.Microtypes
{
    [CreateAssetMenu(order = Consts.ActivationMenuItems, menuName = "Ossify/Uniform Distribution")]
    public sealed class UniformDistribution : SampleDistribution
    {        
        /// <inheritdoc />
        public override SampleHistory CreateHistory() => new (0);

        /// <inheritdoc />
        public override int Distribute(int input, SampleHistory state) 
        {
            var sample = Random.Range(0, input);
            state.AddSample(sample); 
            return sample;
        }
    }
}