using GeneticToolkit.Interfaces;

using System;

namespace GeneticToolkit.Genotypes.Primitive
{
    public class UInt16Genotype : GenericPrimitiveGenotype<UInt16>
    {
        public UInt16Genotype() : base(sizeof(ushort))
        {
            Genes = BitConverter.GetBytes(0);
        }

        public UInt16Genotype(ushort value) : base(sizeof(ushort))
        {
            Genes = BitConverter.GetBytes(value);
        }

        public override IGenotype ShallowCopy()
        {
            return new UInt16Genotype(sizeof(ushort))
            { Genes = this.Genes.Clone() as byte[] };
        }

        public override IGenotype EmptyCopy()
        {
            return new UInt16Genotype(sizeof(ushort));
        }
        public override T EmptyCopy<T>()
        {
            return (T) EmptyCopy();
        }
    }
}
