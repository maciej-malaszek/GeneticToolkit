using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Policies.Stop
{
    public class PopulationDegradation : IStopCondition
    {
        public double MaximumSimilarity { get; set; }
        public bool Satisfied(IEvolutionaryPopulation population)
        {
            return population.Homogeneity >= MaximumSimilarity;
        }

        public void Reset() { }

        public PopulationDegradation(double maximumSimilarity)
        {
            MaximumSimilarity = maximumSimilarity;
        }
    }
}
