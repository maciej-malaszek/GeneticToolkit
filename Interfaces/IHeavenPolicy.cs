using System;
using System.Collections.Generic;

namespace GeneticToolkit.Interfaces
{
    public interface IHeavenPolicy<TFitness> where TFitness : IComparable
    {
        ICollection< IIndividual<TFitness> > Memory { get; }

        void HandleGeneration(IPopulation<TFitness> population);
    }
}
