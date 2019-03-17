using System.Collections.Generic;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Policies.Heaven
{
    public class OneGod : IHeavenPolicy
    {
        private readonly IIndividual[] _memory = new IIndividual[1];

        public ICollection<IIndividual> Memory => _memory;

        public void HandleGeneration(IPopulation population)
        {
            _memory[0] = population.CompareCriteria.GetBetter(_memory[0], population.GetBest());
        }
    }
}
