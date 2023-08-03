using UnityEngine;

namespace Ossify
{
    public abstract class SampleDistribution : Distribution<int, int, SampleHistory>
    {

    }

    public class SampleHistory 
    {       
        public readonly int[] History;

        private int index;

        public SampleHistory(int length) => History = new int[length];

        public int Index => index;

        public void AddSample(int sample)
        {
            History[index] = sample;

            index = (index + 1) % History.Length;
        }
    }

    // public abstract class Distribution : ScriptableObject
    // {
    //     public abstract int HistorySize { get; }
    //
    //     public abstract int Pick(int count, int[] history, ref int historyIndex);
    // }

    public abstract class Distribution<TInput, TOutput, THistory> : ScriptableObject
    {
        public abstract THistory CreateHistory();

        public abstract TOutput Distribute(TInput input, THistory history);
    }
}