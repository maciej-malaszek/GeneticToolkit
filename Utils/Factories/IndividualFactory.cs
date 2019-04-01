using System.Collections.Generic;
using GeneticToolkit.Individuals;
using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils.Data;

namespace GeneticToolkit.Utils.Factories
{
    public class IndividualFactory<TGenotype, TPhenotype> : IndividualFactoryBase
    where TGenotype : IGenotype, new()
    where TPhenotype : IPhenotype, new()
    {

        public IPhenotypeFactory<TPhenotype> Factory { get; set; }

        public IndividualFactory(IDictionary<string, object> parameters)
        {
            Factory = (IPhenotypeFactory<TPhenotype>) parameters["PhenotypeFactory"];
        }

        public IndividualFactory(IPhenotypeFactory<TPhenotype> factory)
        {
            Factory = factory;
        }

        public override IIndividual CreateFromGenotype(IGenotype genotype, IPhenotype phenotype)
        {
            return new Individual(genotype, phenotype);
        }

        public override IIndividual CreateRandomIndividual()
        {
            IGenotype genotype = new TGenotype().Randomized();
            return new Individual(genotype, Factory.Make(genotype));
        }

        public override GeneticAlgorithmParameter Serialize()
        {
            return new GeneticAlgorithmParameter(this)
            {
                Params = new Dictionary<string, object>()
                {
                    { "PhenotypeFactory", Factory.Serialize() }
                }
            };
        }
    }
}
