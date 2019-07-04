using GeneticToolkit.Interfaces;
using System;
using JetBrains.Annotations;

namespace GeneticToolkit.Genotypes.Primitive
{
    [PublicAPI]
    public class UInt64Genotype : GenericPrimitiveGenotype<ulong>
    {
        public UInt64Genotype() : base(sizeof(ulong))
        {
            Genes = BitConverter.GetBytes(0);
        }

        public UInt64Genotype(ulong value) : base(sizeof(ulong))
        {
            Genes = BitConverter.GetBytes(value);
        }

        public override IGenotype ShallowCopy()
        {
            return new UInt64Genotype(sizeof(ulong))
            {
                Genes = Genes.Clone() as byte[],
            };
        }

        public override IGenotype EmptyCopy()
        {
            return new UInt64Genotype(sizeof(ulong));
        }

        public override T EmptyCopy<T>()
        {
            return (T) EmptyCopy();
        }
    }
}