using JetBrains.Annotations;

namespace GeneticToolkit.Interfaces
{
    [PublicAPI]
    public interface IMutation
    {
        void Mutate(IGenotype genotype, IMutationPolicy mutationPolicy, IPopulation population);
    }
}
