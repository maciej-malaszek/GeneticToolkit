using System;
using JetBrains.Annotations;

namespace GeneticToolkit.Interfaces
{
    [PublicAPI]
    public interface IIncompatibilityPolicy
    {
        Func<IPopulation, IIndividual, bool> IsCompatible { get; set; }

        IIndividual GetReplacement(IPopulation population, IIndividual incompatibleIndividual, IGenotype[] parents);
    }
}
