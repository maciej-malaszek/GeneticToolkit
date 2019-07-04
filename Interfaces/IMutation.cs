namespace GeneticToolkit.Interfaces
{
    public interface IMutation
    {
        void Mutate(IGenotype genotype, IMutationPolicy mutationPolicy, IPopulation population);
    }
}
