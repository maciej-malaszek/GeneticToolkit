using System;
using System.Collections.Generic;
using System.Linq;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Utils.Statistics
{
    public class ChangeHistory : IStatisticUtility
    {
        public List<double> History { get; set; } = new List<double>();

        public void UpdateData(IPopulation population)
        {
            IFitnessFunction f = population.FitnessFunction;
            History.Add(History.Count == 0
                ? f.GetValue(population.GetBest())
                : f.GetValue(population.GetBest()) - History.Last());
        }

        public void Reset()
        {
            History.Clear();
        }

        public double GetAverageImprovement(int startIndex, uint generations)
        {
            if(startIndex + generations > History.Count)
                throw new IndexOutOfRangeException("History to short to calculate such statistic");

            double sum = 0;
            for(int i = startIndex; i < startIndex + generations; i++)
                sum += History[i];
            sum /= generations;
            return sum;
        }
    }
}
