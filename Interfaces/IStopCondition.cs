namespace GeneticToolkit.Interfaces
{
    public interface IStopCondition
    {
        bool Satisfied(IEvolutionaryPopulation population);
        void Reset();
    }
}
