using System;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Policies.Stop
{
    public class GenerationsLimit : IStopCondition
    {
        private readonly uint _limit;

        public bool Satisfied(IPopulation population)
        {
            if(population == null)
                throw new NullReferenceException("Population has not been initialized!");
            return population.Generation >= _limit;
        }

        public void Reset() {  }

        public GenerationsLimit(uint generationsLimit = 1000)
        {
            _limit = generationsLimit;
        }

    }
}
