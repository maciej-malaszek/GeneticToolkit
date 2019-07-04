using JetBrains.Annotations;

namespace GeneticToolkit.Interfaces
{
    [PublicAPI]
    public interface IMutationPolicy
    {
        float GetMutationChance(IPopulation population);

        float MutatedGenesPercent { get; }
    }
}
