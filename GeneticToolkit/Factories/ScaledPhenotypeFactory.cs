using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils;
using JetBrains.Annotations;

namespace GeneticToolkit.Factories
{
    [PublicAPI]
    public class ScaledPhenotypeFactory<TPhenotype, TOutput> : IPhenotypeFactory<TPhenotype>
        where TPhenotype : IScaledPhenotype<TOutput>, new()
    {
        public Range<TOutput> Range { get; set; }

        public ScaledPhenotypeFactory(Range<TOutput> range)
        {
            Range = range;
        }
        public TPhenotype Make(IGenotype genotype)
        {
            return new()
            {
                Genotype = genotype,
                Scale = Range
            };
        }
    }
}
