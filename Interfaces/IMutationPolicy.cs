namespace GeneticToolkit.Interfaces
{
    public interface IMutationPolicy
    {
        float GetMutationChance(IPopulation population);

        float MutatedGenesPercent { get; }
    }
}
