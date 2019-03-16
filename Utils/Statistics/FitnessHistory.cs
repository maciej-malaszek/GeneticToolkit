using System;
using System.Collections.Generic;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Utils.Statistics
{
    public class FitnessHistory<TFitness> : IStatisticUtility<TFitness> where TFitness : IComparable
    {
        protected IList<TFitness> History { get; set; } = new List<TFitness>();

        public TFitness this[int indexer] => History[indexer];

        public void UpdateData(IPopulation<TFitness> population)
        {
            TFitness functionValue = population.CompareCriteria.FitnessFunction.GetValue(population.GetBest());
            History.Add(functionValue);
        }
    }
}
