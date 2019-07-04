using System.Collections.Generic;
using GeneticToolkit.Interfaces;
using JetBrains.Annotations;

namespace GeneticToolkit.Utils.Statistics
{
    [PublicAPI]
    public class FitnessHistory : IStatisticUtility
    {
        protected List<double> History { get; set; } = new List<double>();

        public int Length => History.Count;

        public double this[int indexer] { get => History[indexer]; set => History[indexer] = value; }

        public double GetValue(int generation)
        {
            return generation <= Length ? History[generation] : 0;
        }

        public void UpdateData(IEvolutionaryPopulation population)
        {
            double functionValue = population.FitnessFunction.GetValue(population.Best);
            History.Add(functionValue);
        }

        public void Reset()
        {
            History.Clear();
        }
    }
}
