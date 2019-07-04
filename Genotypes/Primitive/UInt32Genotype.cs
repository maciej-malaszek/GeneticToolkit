using GeneticToolkit.Interfaces;

using System;

namespace GeneticToolkit.Genotypes.Primitive
{
    public class UInt32Genotype : GenericPrimitiveGenotype<UInt32>
    {

        public UInt32Genotype() : base(sizeof(uint))
        {
            Genes = BitConverter.GetBytes(0);
        }

        public UInt32Genotype(uint value) : base(sizeof(uint))
        {
            Genes = BitConverter.GetBytes(value);
        }

        public override IGenotype ShallowCopy()
        {
            return new UInt32Genotype(sizeof(uint))
            {
                Genes = this.Genes.Clone() as byte[],
            };
        }

        public override IGenotype EmptyCopy()
        {
            return new UInt32Genotype(sizeof(uint));
        }
        public override T EmptyCopy<T>()
        {
            return (T) EmptyCopy();
        }
    }
}