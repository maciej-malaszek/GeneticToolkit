using System.Runtime.Serialization;
using GeneticToolkit.Interfaces;
using GeneticToolkit.Phenotypes.Collective;
using JetBrains.Annotations;

namespace GeneticToolkit.Utils.Factories
{
    [PublicAPI]
    public class CollectivePhenotypeFactory<T> : IPhenotypeFactory<CollectivePhenotype<T>>
        where T : struct,IGeneticallySerializable
    {
        public CollectivePhenotype<T> Make(IGenotype genotype)
        {
            return new CollectivePhenotype<T>()
            {
                Genotype = genotype
            };
        }
    }
}