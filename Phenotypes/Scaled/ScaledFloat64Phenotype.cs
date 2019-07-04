using GeneticToolkit.Interfaces;
using GeneticToolkit.Phenotypes.Primitive;
using GeneticToolkit.Utils;

using System;

namespace GeneticToolkit.Phenotypes.Scaled
{
    public class ScaledFloat64Phenotype: PrimitivePhenotype<double>, IScaledPhenotype<double>
    {
        public Range<double> Scale { get; set; }
        public double Length => Scale.High - Scale.Low;
        public double Step => Length / ulong.MaxValue;

        public override double GetValue()
        {
            double result = BitConverter.ToUInt64(Genotype.Genes) * Step + Scale.Low;
            return result;
        }

        public override IPhenotype ShallowCopy()
        {
            return new ScaledFloat64Phenotype()
            {
                Scale = Scale,
                Genotype = Genotype.ShallowCopy()
            };
        }
    }
}
