using GeneticToolkit.Interfaces;
using JetBrains.Annotations;

namespace GeneticToolkit.Utils.Factories
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
            return new TPhenotype()
            {
                Genotype = genotype,
                Scale = Range
            };
        }
    }
}
