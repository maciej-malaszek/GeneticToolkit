using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using GeneticToolkit.Interfaces;
using JetBrains.Annotations;

namespace GeneticToolkit.Genotypes.Collective
{
    [PublicAPI]
    public class CollectiveGenotype<T> : GenotypeBase where T : struct, IGeneticallySerializable
    {
        public CollectiveGenotype()
        {
            T t = default;
            Genes = t.Serialize();
        }
        public CollectiveGenotype(int size)
        {
            Genes = new byte[size];
        }
        public CollectiveGenotype(T initialValue)
        {
            Genes = initialValue.Serialize();
        }

        public override IGenotype ShallowCopy()
        {
            return new CollectiveGenotype<T>(Genes.Length) {Genes = Genes.Clone() as byte[]};
        }

        public override IGenotype EmptyCopy()
        {
            return new CollectiveGenotype<T>(Genes.Length);
        }

        public override TX EmptyCopy<TX>()
        {
            return (TX) EmptyCopy();
        }

    }
}