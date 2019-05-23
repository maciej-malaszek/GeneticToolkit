using GeneticToolkit.Interfaces;

using System;

namespace GeneticToolkit.Genotypes.Primitive
{
    public class UInt64Genotype : GenericPrimitiveGenotype<UInt64>
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
                Genes = this.Genes.Clone() as byte[],
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