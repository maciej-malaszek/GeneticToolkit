using GeneticToolkit.Interfaces;

using JetBrains.Annotations;

using System;
using System.Collections.Generic;

namespace GeneticToolkit.Selections
{
    [PublicAPI]
    public class RankRoulette : ISelectionMethod
    {
        public ICompareCriteria CompareCriteria { get; set; }

        public Func<int, double> RankingValueFunc { get; set; }

        protected Random RandomNumberGenerator { get; set; } = new Random();

        protected IPopulation Population { get; set; }

        protected uint CurrentGeneration { get; set; }

        protected bool Deprecated { get; set; } = true;

        protected double[] FitnessList;

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

            if (Deprecated)
                Update(population);

            double randomValue = RandomNumberGenerator.NextDouble() * Sum;
            int iterator = population.Size - 1;

            if (MinValue == null)
                return Population[iterator];

            double localSum = FitnessList[iterator] - MinValue.Value;
            while (localSum < randomValue && iterator > 0)
                localSum += FitnessList[--iterator] - MinValue.Value;

            return iterator < population.Size ? Population[iterator] : Population.HeavenPolicy.Memory[iterator - population.Size];
        }

        public void Update(IPopulation population)
        {
            Population = population;
            Population.SortDescending();
            CompareCriteria = Population.CompareCriteria;
            CurrentGeneration = Population.Generation;
            FitnessList = new double[Population.Size + (Population.HeavenPolicy.UseInCrossover ? Population.HeavenPolicy.Size : 0)];
            MinValue = null;

            // Population is already sorted from best to worse
            for (var i = 0; i < Population.Size; i++)
            {
                double functionValue = RankingValueFunc(CompareCriteria.OptimizationMode == EOptimizationMode.Minimize ? i : Population.Size - 1 - i);
                if (!MinValue.HasValue || functionValue < MinValue)
                    MinValue = functionValue;
                FitnessList[i] = functionValue;
            }
            if (Population.HeavenPolicy.UseInCrossover && population.Generation > 0)
                for (int i = Population.Size; i < Population.Size + Population.HeavenPolicy.Size; i++)
                {
                    double functionValue = Population.HeavenPolicy.Memory[i - Population.Size].Value;
                    if (!MinValue.HasValue || functionValue < MinValue)
                        MinValue = functionValue;
                    FitnessList[i] = functionValue;
                }


            Sum = 0;
            foreach (double x in FitnessList)
                if (MinValue != null)
                    Sum += x - MinValue.Value;
            Deprecated = false;
        }
    }
}