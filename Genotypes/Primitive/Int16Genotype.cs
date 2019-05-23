using GeneticToolkit.Interfaces;

using System;

namespace GeneticToolkit.Genotypes.Primitive
{
    public class Int16Genotype : PrimitiveGenotype
    {
        public Int16Genotype() : base(sizeof(short))
        {
            Genes = BitConverter.GetBytes(0);
        }

        public Int16Genotype(short value) : base(sizeof(short))
        {
            Genes = BitConverter.GetBytes(value);
        }

        public override IGenotype ShallowCopy()
        {
            return new Int16Genotype(sizeof(short))
            {
                Genes = this.Genes.Clone() as byte[],
            };
        }

        public override IGenotype EmptyCopy()
        {
            return new Int16Genotype(sizeof(short));
        }
        public override T EmptyCopy<T>()
        {
            return (T) EmptyCopy();
        }
    }
}
