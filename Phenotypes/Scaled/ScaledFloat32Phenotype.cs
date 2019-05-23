using GeneticToolkit.Interfaces;
using GeneticToolkit.Phenotypes.Primitive;
using GeneticToolkit.Utils;

using System;

namespace GeneticToolkit.Phenotypes.Scaled
{
    public class ScaledFloat32Phenotype : PrimitivePhenotype<float>, IScaledPhenotype<float>
    {
        public Range<float> Scale { get; set; }
        public float Length => Scale.High - Scale.Low;
        public float Step => Length / uint.MaxValue;

        public override float GetValue()
        {
            float result = BitConverter.ToUInt32(Genotype.Genes) * Step + Scale.Low;
            return result;
        }

        public override IPhenotype ShallowCopy()
        {
            return new ScaledFloat32Phenotype()
            {
                Scale = Scale,
                Genotype = Genotype.ShallowCopy()
            };
        }
    }
}
