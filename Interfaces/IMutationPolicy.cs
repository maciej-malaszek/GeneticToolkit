namespace GeneticToolkit.Interfaces
{
    public interface IMutationPolicy
    {
        float MutationChance { get; }

        uint MutatedGenesCount { get; }
    }
}
