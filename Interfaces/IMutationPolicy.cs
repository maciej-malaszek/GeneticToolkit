using System;

namespace GeneticToolkit.Interfaces
{
    public interface IMutationPolicy<TFitness> where TFitness:IComparable
    {
        float MutationChance { get; }

        uint MutatedGenesCount { get; }
    }
}
