using GeneticToolkit.Interfaces;
using GeneticToolkit.Phenotypes.Collective;
using JetBrains.Annotations;

namespace GeneticToolkit.Factories
{
    [PublicAPI]
    public class CollectivePhenotypeFactory<T> : IPhenotypeFactory<CollectivePhenotype<T>>
        where T : IGeneticallySerializable, new()
    {
        public CollectivePhenotype<T> Make(IGenotype genotype)
        {
            return new()
            {
                Genotype = genotype
            };
        }
    }
}