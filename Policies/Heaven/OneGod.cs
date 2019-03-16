using System;
using System.Collections.Generic;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Policies.Heaven
{
    public class OneGod<TFitness> : IHeavenPolicy<TFitness> where TFitness:IComparable
    {
        private readonly  IIndividual<TFitness> [] _memory = new  IIndividual<TFitness> [1];

        public ICollection< IIndividual<TFitness> > Memory => _memory;

        public void HandleGeneration(IPopulation<TFitness> population)
        {
            _memory[0] = population.CompareCriteria.GetBetter(_memory[0], population.GetBest());
        }
    }
}
