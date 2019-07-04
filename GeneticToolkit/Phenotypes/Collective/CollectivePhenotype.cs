using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using GeneticToolkit.Genotypes;
using GeneticToolkit.Genotypes.Collective;
using GeneticToolkit.Interfaces;
using JetBrains.Annotations;

namespace GeneticToolkit.Phenotypes.Collective
{
    [PublicAPI]
    public class CollectivePhenotype<T> : IPhenotype where T : struct, IGeneticallySerializable
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