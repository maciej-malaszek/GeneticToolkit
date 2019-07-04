using JetBrains.Annotations;

namespace GeneticToolkit.Interfaces
{
    [PublicAPI]
    public interface IStopCondition
    {
        bool Satisfied(IEvolutionaryPopulation population);
        void Reset();
    }
}
