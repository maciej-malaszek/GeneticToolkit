using GeneticToolkit.Interfaces;

using System;

namespace GeneticToolkit.Genotypes.Primitive
{
    public class Int64Genotype : GenericPrimitiveGenotype<Int64>
    {
        public Int64Genotype() : base(sizeof(long))
        {
            Genes = BitConverter.GetBytes(0);
        }

        public Int64Genotype(long value) : base(sizeof(long))
        {
            Genes = BitConverter.GetBytes(value);
        }
        
        public override IGenotype ShallowCopy()
        {
            return new Int64Genotype(sizeof(long))
            {
                Genes = this.Genes.Clone() as byte[],
            };
        }

        public override IGenotype EmptyCopy()
        {
            return new Int64Genotype(sizeof(long));
        }
        public override T EmptyCopy<T>()
        {
            return (T) EmptyCopy();
        }
    }
}
