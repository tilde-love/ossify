using UnityEngine;

namespace Ossify
{
    public abstract class Distribution : ScriptableObject
    {
        public abstract int HistorySize { get; } 

        public abstract int Pick(int count, int[] history, ref int historyIndex);
    }
}