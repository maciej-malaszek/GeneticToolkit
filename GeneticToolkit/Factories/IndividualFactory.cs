using GeneticToolkit.Individuals;
using GeneticToolkit.Interfaces;
using JetBrains.Annotations;

namespace GeneticToolkit.Factories
{
    [PublicAPI]
    public class IndividualFactory<TGenotype, TPhenotype,TFitnessFunctionFactory> : IndividualFactoryBase
        where TGenotype : IGenotype, new()
        where TPhenotype : IPhenotype, new()
        where TFitnessFunctionFactory : IFitnessFunctionFactory, new()
    {
        public IPhenotypeFactory<TPhenotype> Factory { get; set; }

        public IndividualFactory(IPhenotypeFactory<TPhenotype> factory)
        {
            Factory = factory;
        }

        public override IIndividual CreateFromGenotype(IGenotype genotype)
        {
            return new Individual<TFitnessFunctionFactory>(genotype, Factory.Make(genotype));
        }

        public override IIndividual CreateRandomIndividual()
        {
            var genotype = new TGenotype().Randomized();
            return new Individual<TFitnessFunctionFactory>(genotype, Factory.Make(genotype));
        }
    }
}