using GeneticToolkit.Interfaces;

using System;
using JetBrains.Annotations;

namespace GeneticToolkit.Genotypes.Primitive
{
    [PublicAPI]
    public class Int32Genotype : GenericPrimitiveGenotype<int>
    {
        public Int32Genotype() : base(sizeof(int))
        {
            Genes = BitConverter.GetBytes(0);
        }

        public Int32Genotype(int value) : base(sizeof(int))
        {
            Genes = BitConverter.GetBytes(value);
        }

        public override IGenotype ShallowCopy()
        {
            return new Int32Genotype(sizeof(int))
            {
                Genes = Genes.Clone() as byte[],
            };
        }

        public override IGenotype EmptyCopy()
        {
            return new Int32Genotype(sizeof(int));
        }
        public override T EmptyCopy<T>()
        {
            return (T) EmptyCopy();
        }
    }
}
