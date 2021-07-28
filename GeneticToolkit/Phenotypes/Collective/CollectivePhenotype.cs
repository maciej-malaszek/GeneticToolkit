using GeneticToolkit.Genotypes.Collective;
using GeneticToolkit.Interfaces;
using JetBrains.Annotations;

namespace GeneticToolkit.Phenotypes.Collective
{
    [PublicAPI]
    public class CollectivePhenotype<T> : IPhenotype where T : IGeneticallySerializable, new()
    {
        private CollectiveGenotype<T> _genotype;

        public IGenotype Genotype
        {
            get => _genotype;
            set => _genotype = value as CollectiveGenotype<T>;
        }

        public T GetValue()
        {
            return (T) new T().Deserialize(_genotype.Genes);
        }

        public IPhenotype ShallowCopy()
        {
            return new CollectivePhenotype<T>
            {
                Genotype = Genotype
            };
        }
    }
}