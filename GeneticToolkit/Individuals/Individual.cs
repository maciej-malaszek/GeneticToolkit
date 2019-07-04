using GeneticToolkit.Interfaces;
using JetBrains.Annotations;

namespace GeneticToolkit.Individuals
{
    [PublicAPI]
    public class Individual : IIndividual
    {
        public IGenotype Genotype { get; set; }

        public IPhenotype Phenotype { get; }

        public int CompareTo(IIndividual other, ICompareCriteria criteria)
        {
            return criteria.Compare(this, other);
        }

        public Individual(IGenotype genotype, IPhenotype phenotype)
        {
            Genotype = genotype;
            Phenotype = phenotype;
            Phenotype.Genotype = genotype;
        }
    }
}
