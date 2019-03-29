using GeneticToolkit.Individuals;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Utils.Factories
{
    public class IndividualFactory<TGenotype, TPhenotype> : IndividualFactoryBase
    where TGenotype : IGenotype, new()
    where TPhenotype : IPhenotype, new()
    {

        public IPhenotypeFactory<TPhenotype> Factory { get; set; }

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
    }
}
