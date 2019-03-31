using System;

namespace GeneticToolkit.Interfaces
{
    public interface IIncompatibilityPolicy : IGeneticSerializable
    {
        Func<IPopulation, IIndividual, bool> IsCompatible { get; set; }

        IIndividual GetReplacement(IPopulation population, IIndividual incompatibleIndividual, IIndividual[] parents);
    }
}
