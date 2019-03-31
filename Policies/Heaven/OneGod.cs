using System.Collections.Generic;
using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils.Data;

namespace GeneticToolkit.Policies.Heaven
{
    public class OneGod : IHeavenPolicy
    {
        private readonly IIndividual[] _memory = new IIndividual[1];

        public ICollection<IIndividual> Memory => _memory;

        public OneGod() { }

        public OneGod(IDictionary<string, object> parameters) { }

        public void HandleGeneration(IPopulation population)
        {
            _memory[0] = population.CompareCriteria.GetBetter(_memory[0], population.GetBest());
        }

        public GeneticAlgorithmParameter Serialize()
        {
            return new GeneticAlgorithmParameter(this);
        }
    }
}
