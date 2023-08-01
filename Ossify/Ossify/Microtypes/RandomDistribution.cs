using UnityEngine;

namespace Ossify
{    
    [CreateAssetMenu(order = Consts.ActivationMenuItems, menuName = "Variables/Random Distribution")]
    public sealed class RandomDistribution : Distribution 
    {
        /// <inheritdoc />
        public override int HistorySize => 0;

        /// <inheritdoc />
        public override int Pick(int count, int[] history, ref int historyIndex) => Random.Range(0, count);
    }
}