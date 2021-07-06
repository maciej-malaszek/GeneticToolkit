using GeneticToolkit.Interfaces;

using System;
using JetBrains.Annotations;

namespace GeneticToolkit.Genotypes.Primitive
{
    [PublicAPI]
    public class Float64Genotype : GenericPrimitiveGenotype<double>
    {
        public Float64Genotype(): base(sizeof(double)) {}
        public Float64Genotype(int size) : base(size) { }

        public Float64Genotype(byte[] bytes) : base(bytes) { }

        public Float64Genotype(double value) : base(sizeof(double))
        {
            Genes = BitConverter.GetBytes(value);
        }

        public override IGenotype ShallowCopy()
        {
            return new Float64Genotype(sizeof(double))
            {
                Genes = Genes.Clone() as byte[],
            };
        }

        public override IGenotype EmptyCopy()
        {
            return new Float64Genotype(sizeof(double));
        }

        public override T EmptyCopy<T>()
        {
            return (T) EmptyCopy();
        }
    }
}
