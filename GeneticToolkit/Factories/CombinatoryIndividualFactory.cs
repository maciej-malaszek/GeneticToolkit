using GeneticToolkit.Genotypes.Collective.Combinatory;
using GeneticToolkit.Individuals;
using GeneticToolkit.Interfaces;
using GeneticToolkit.Phenotypes.Collective.Combinatory;
using JetBrains.Annotations;

namespace GeneticToolkit.Factories
{

    [PublicAPI]
    public class CombinatoryIndividualFactory<TGenotype, TPhenotype, TFitnessFunctionFactory> : IndividualFactoryBase
        where TGenotype : CombinatoryGenotype, new()
        where TPhenotype : PermutationPhenotype, new()
        where TFitnessFunctionFactory : IFitnessFunctionFactory, new()
    {
        public IPhenotypeFactory<TPhenotype> Factory { get; set; }
        private static IFitnessFunction _fitnessFunction;
        public int PermutationElementsCount { get; set; }

        public CombinatoryIndividualFactory(IPhenotypeFactory<TPhenotype> factory, int permutationElementsCount)
        {
            Factory = factory;
            PermutationElementsCount = permutationElementsCount;
            _fitnessFunction ??= new TFitnessFunctionFactory().Make();
        }

        public override IIndividual CreateFromGenotype(IGenotype genotype)
        {
            return new Individual<TFitnessFunctionFactory>(genotype, Factory.Make(genotype));
        }

        public override IIndividual CreateRandomIndividual()
        {
            var genotype = new TGenotype {Value = new short[PermutationElementsCount]}.Randomized();
            return new Individual<TFitnessFunctionFactory>(genotype, Factory.Make(genotype));
        }
    }
}