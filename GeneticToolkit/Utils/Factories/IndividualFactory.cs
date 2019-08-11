using GeneticToolkit.Individuals;
using GeneticToolkit.Interfaces;
using JetBrains.Annotations;

namespace GeneticToolkit.Utils.Factories
{
    [PublicAPI]
    public class IndividualFactory<TGenotype, TPhenotype> : IndividualFactoryBase
        where TGenotype : IGenotype, new()
        where TPhenotype : IPhenotype, new()
    {
        public IPhenotypeFactory<TPhenotype> Factory { get; set; }
        public IFitnessFunction FitnessFunction { get; set; }

        public IndividualFactory(IPhenotypeFactory<TPhenotype> factory, IFitnessFunction fitnessFunction)
        {
            Factory = factory;
            FitnessFunction = fitnessFunction;
        }

        public override IIndividual CreateFromGenotype(IGenotype genotype)
        {
            return new Individual(genotype, Factory.Make(genotype),FitnessFunction);
        }

        public override IIndividual CreateRandomIndividual()
        {
            var genotype = new TGenotype().Randomized();
            return new Individual(genotype, Factory.Make(genotype),FitnessFunction);
        }
    }
}