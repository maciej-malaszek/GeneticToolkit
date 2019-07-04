using GeneticToolkit.Genotypes.Collective.Combinatory;
using GeneticToolkit.Individuals;
using GeneticToolkit.Interfaces;
using GeneticToolkit.Phenotypes.Collective.Combinatory;
using JetBrains.Annotations;

namespace GeneticToolkit.Utils.Factories
{
    [PublicAPI]
    public class CombinatoryIndividualFactory<TGenotype, TPhenotype> : IndividualFactoryBase
        where TGenotype : CombinatoryGenotype, new()
        where TPhenotype : PermutationPhenotype, new()
    {
        public IPhenotypeFactory<TPhenotype> Factory { get; set; }

        public int PermutationElementsCount { get; set; }

        public CombinatoryIndividualFactory(IPhenotypeFactory<TPhenotype> factory, int permutationElementsCount)
        {
            Factory = factory;
            PermutationElementsCount = permutationElementsCount;
        }

        public override IIndividual CreateFromGenotype(IGenotype genotype)
        {
            return new Individual(genotype, Factory.Make(genotype));
        }

        public override IIndividual CreateRandomIndividual()
        {
            var genotype = new TGenotype {Value = new short[PermutationElementsCount]}.Randomized();
            return new Individual(genotype, Factory.Make(genotype));
        }
    }
}