using System;

namespace GeneticToolkit.Interfaces
{
    public interface IStopCondition<TFitness> where TFitness:IComparable
    {
        bool Satisfied(IPopulation<TFitness> population);
    }
}
