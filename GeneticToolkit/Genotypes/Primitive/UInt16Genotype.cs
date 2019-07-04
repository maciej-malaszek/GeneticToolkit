using GeneticToolkit.Interfaces;
using System;
using JetBrains.Annotations;

namespace GeneticToolkit.Genotypes.Primitive
{
    [PublicAPI]
    public class UInt16Genotype : GenericPrimitiveGenotype<ushort>
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
                {Genes = Genes.Clone() as byte[]};
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