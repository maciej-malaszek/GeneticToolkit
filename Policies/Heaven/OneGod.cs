using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Policies.Heaven
{
    public class OneGod : IHeavenPolicy
    {
        public IIndividual[] Memory { get; } = new IIndividual[1];

        public OneGod() { }

        public void HandleGeneration(IEvolutionaryPopulation population)
        {
            Memory[0] = population.CompareCriteria.GetBetter(Memory[0], population.Best);
        }
    }
}
