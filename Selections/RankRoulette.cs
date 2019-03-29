using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Selections
{
    public class RankRoulette : ISelectionMethod
    {
        public ICompareCriteria CompareCriteria { get; set; }

        public Func<int, double> RankingValueFunc;

        protected Random RandomNumberGenerator { get; set; } = new Random();

        protected IPopulation Population { get; set; }

        protected List<IIndividual> SortedList = new List<IIndividual>();

        protected uint CurrentGeneration { get; set; }

        protected bool Deprecated { get; set; } = true;

        protected IList<double> FitnessList = new List<double>();
       
        protected double? MinValue { get; set; }

        protected double Sum { get; set; }


        public RankRoulette(ICompareCriteria compareCriteria, Func<int, double> rankingValueFunc)
        {
            CompareCriteria = compareCriteria;
            RankingValueFunc = rankingValueFunc;
        }

        public IIndividual Select(IPopulation population)
        {
            Deprecated = population != Population || population.Generation != CurrentGeneration;

            if(Deprecated)
                Update(population);

            double randomValue = RandomNumberGenerator.NextDouble() * Sum;
            int iterator = 0;

            Debug.Assert(MinValue != null, nameof(MinValue) + " != null");
            double localSum = FitnessList[iterator] - MinValue.Value;

            while(localSum < randomValue && iterator < Population.Size - 1)
                localSum += FitnessList[++iterator] - MinValue.Value;

            return Population[iterator];
        }

        public void Update(IPopulation population)
        {
            Population = population;
            CompareCriteria = Population.CompareCriteria;
            CurrentGeneration = Population.Generation;
            FitnessList.Clear();
            SortedList = Population.ToList();
            SortedList.Sort( (individual, individual1) =>
            {
                var result = CompareCriteria.Compare(individual, individual1);
                return result;
            });

            MinValue = null;
            for(int i = 0; i < SortedList.Count; i++)
            {
                double functionValue = RankingValueFunc(i);
                if(functionValue < MinValue || MinValue == null)
                    MinValue = functionValue;
                FitnessList.Add(functionValue);
            }

            Sum = FitnessList.Sum(x =>
            {
                Debug.Assert(MinValue != null, nameof(MinValue) + " != null");
                return x - MinValue.Value;
            });

            Deprecated = false;
        }


    }
}
