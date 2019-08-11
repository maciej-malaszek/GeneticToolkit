using JetBrains.Annotations;

namespace GeneticToolkit.Interfaces
{
    [PublicAPI]
    public interface IHeavenPolicy
    {
        IIndividual[] Memory { get; }
        bool UseInCrossover { get; set; }
        int Size { get; }
        void HandleGeneration(IEvolutionaryPopulation population);
    }
}
