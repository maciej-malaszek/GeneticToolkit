namespace GeneticToolkit.Interfaces
{
    public interface IHeavenPolicy
    {
        IIndividual[] Memory { get; }

        void HandleGeneration(IEvolutionaryPopulation population);
    }
}
