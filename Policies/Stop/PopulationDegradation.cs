using System.Collections.Generic;
using System.Linq;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Policies.Stop
{
    public class PopulationDegradation : IStopCondition
    {
        public double MaximumSimilarity { get; set; }
        public bool Satisfied(IPopulation population)
        {
            IList<IGenotype> genotypes = population.Select(x => x.Genotype).OrderByDescending(x => x).ToList();
            return genotypes[0].SimilarityCheck(genotypes[genotypes.Count - 1]) >= MaximumSimilarity;
        }

        public void Reset() { }

        public PopulationDegradation(double maximumSimilarity)
        {
            MaximumSimilarity = maximumSimilarity;
        }
    }
}
