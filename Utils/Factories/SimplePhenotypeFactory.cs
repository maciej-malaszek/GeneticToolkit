using System.Collections.Generic;
using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils.Data;

namespace GeneticToolkit.Utils.Factories
{
    public class SimplePhenotypeFactory<TPhenotype> : IPhenotypeFactory<TPhenotype>
        where TPhenotype : IPhenotype, new()

    {
        public SimplePhenotypeFactory(IDictionary<string, object> parameters)
        {
        }

        public TPhenotype Make(IGenotype genotype)
        {
            return new TPhenotype()
            {
                Genotype = genotype
            };
        }

        public GeneticAlgorithmParameter Serialize()
        {
            return new GeneticAlgorithmParameter(this);
        }
    }
}
