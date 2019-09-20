using System;
using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils.Exceptions;
using JetBrains.Annotations;

namespace GeneticToolkit.Policies.Stop
{
    [PublicAPI]
    public class GenerationsLimit : IStopCondition
    {
        private readonly uint _limit;

        public bool Satisfied(IEvolutionaryPopulation population)
        {
            if (population == null)
                throw new PopulationNotInitializedException();
            return population.Generation >= _limit;
        }

        public void Reset() {  }

        public GenerationsLimit(uint generationsLimit = 1000)
        {
            _limit = generationsLimit;
        }

    }
}
