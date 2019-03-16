using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Utils.Factories
{
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
