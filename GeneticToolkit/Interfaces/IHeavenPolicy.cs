using JetBrains.Annotations;

namespace GeneticToolkit.Interfaces
{
    [PublicAPI]
    public interface IHeavenPolicy
    {
        IIndividual[] Memory { get; }

        void HandleGeneration(IEvolutionaryPopulation population);
    }
}
