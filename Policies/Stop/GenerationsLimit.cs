using System;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Policies.Stop
{
    public class GenerationsLimit<TFitness> : IStopCondition<TFitness> where TFitness:IComparable
    {
        private readonly uint _limit;

        public bool Satisfied(IPopulation<TFitness> population)
        {
            if(population == null)
                throw new NullReferenceException("Population has not been initialized!");
            return population.Generation >= _limit;
        }

        public GenerationsLimit( uint generationsLimit = 1000)
        {
            _limit = generationsLimit;
        }
    }
}
