using System;

namespace GeneticToolkit.Interfaces
{
    public interface IIncompatibilityPolicy
    {
        Func<IPopulation, IIndividual, bool> IsCompatible { get; set; }

        IIndividual GetReplacement(IPopulation population, IIndividual incompatibleIndividual, IGenotype[] parents);
    }
}
