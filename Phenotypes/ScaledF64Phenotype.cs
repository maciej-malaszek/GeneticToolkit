using System;
using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils;

namespace GeneticToolkit.Phenotypes
{
    public class ScaledF64Phenotype : F64Phenotype, IScaledPhenotype<double>
    {
        public Range<double> Scale { get; set; }

        public double Length => Scale.High - Scale.Low;

        public double Step => Length / uint.MaxValue;

        public override double GetValue()
        {
            double result = BitConverter.ToUInt32(_genotype.Bytes) * Step + Scale.Low;
            return result;
        }

        public override IPhenotype ShallowCopy()
        {
            return new ScaledF64Phenotype()
            {
                Scale = Scale,
                Genotype = _genotype.ShallowCopy()
            };
        }
    }
}
