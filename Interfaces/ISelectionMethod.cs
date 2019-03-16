using System;

namespace GeneticToolkit.Interfaces
{
    public interface ISelectionMethod<TFitness> where TFitness : IComparable
    {
        ICompareCriteria<TFitness> CompareCriteria { get; set; }

         IIndividual<TFitness>  Select(IPopulation<TFitness> population);
    }
}
