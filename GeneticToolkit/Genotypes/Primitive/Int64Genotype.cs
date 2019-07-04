using GeneticToolkit.Interfaces;

using System;
using JetBrains.Annotations;

namespace GeneticToolkit.Genotypes.Primitive
{
    [PublicAPI]
    public class Int64Genotype : GenericPrimitiveGenotype<long>
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
                Genes = Genes.Clone() as byte[],
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
