using GeneticToolkit.Interfaces;
using JetBrains.Annotations;

namespace GeneticToolkit.Utils.Factories
{
    [PublicAPI]
    public class SimplePhenotypeFactory<TPhenotype> : IPhenotypeFactory<TPhenotype>
        where TPhenotype : IPhenotype, new()

    {
        public TPhenotype Make(IGenotype genotype)
        {
            return new TPhenotype()
            {
                Genotype = genotype
            };
        }
    }
}