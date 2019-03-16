using System;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Policies.Mutation
{
    public class SimpleMutation<TFitness> : IMutationPolicy<TFitness> where TFitness:IComparable
    {
        public float MutationChance { get; protected set; }
        public uint MutatedGenesCount { get; protected set; }

        public SimpleMutation(float mutationChance = 0.1f, uint mutatedGenesCount = 1)
        {
            MutationChance = mutationChance;
            MutatedGenesCount = mutatedGenesCount;
        }
    }
}
