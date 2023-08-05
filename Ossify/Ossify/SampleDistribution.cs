using System;
using UnityEngine;

namespace Ossify
{
    public abstract class SampleDistribution : ScriptableObject // Distribution<int, int, SampleHistory>
    {
        public abstract SampleHistory CreateHistory();

        public abstract int Distribute(int count, SampleHistory history);
    }

    public class SampleHistory 
    {       
        public readonly int[] History;

        private int index;

        public SampleHistory(int length) => History = new int[length];

        public int Index => index;

        public void AddSample(int sample)
        {
            if (History.Length == 0) return;

            History[index] = sample;

            index = (index + 1) % History.Length;
        }
    }

    // public abstract class WeightedDistribution : Distribution<float, float[], float[]>
    // {
    //
    // }

    // public abstract class Distribution<TInput, TOutput, TState> : ScriptableObject
    // {
    //     public abstract TOutput Distribute(TInput input, TState state);
    //
    //     private readonly Custodian<Artifact> custodian;
    //
    //     public Distribution()
    //     {
    //         custodian = new Custodian<Artifact>(ArtifactFactory);
    //     }
    //
    //     private Artifact ArtifactFactory(Custodian<Artifact>.Destructor destructor)
    //     {
    //         
    //     }
    //
    //     public class Artifact : IDisposable
    //     {
    //         void IDisposable.Dispose()
    //         {
    //         }
    //     }
    // }
}