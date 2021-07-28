using GeneticToolkit.Interfaces;
using JetBrains.Annotations;
using System;

namespace GeneticToolkit.Selections
{
    [PublicAPI]
    public class RankRoulette<TRankingFunctionFactory> : ISelectionMethod where TRankingFunctionFactory : IRankingFunctionFactory, new()
    {
        public ICompareCriteria CompareCriteria { get; set; }

        private static Func<int, double> _rankingValueFunc;

        protected Random RandomNumberGenerator { get; set; } = new();

        protected IPopulation Population { get; set; }

        protected uint CurrentGeneration { get; set; }

        protected bool Deprecated { get; set; } = true;

        protected double[] FitnessList;

        protected double? MinValue { get; set; }

        protected double Sum { get; set; }

        public RankRoulette()
        {
        }

        public RankRoulette(ICompareCriteria compareCriteria)
        {
            CompareCriteria = compareCriteria;
            _rankingValueFunc ??= new TRankingFunctionFactory().Make();
        }

        public IIndividual Select(IPopulation population)
        {
            Deprecated = population != Population || population.Generation != CurrentGeneration;

            if (Deprecated)
            {
                Update(population);
            }

            var randomValue = RandomNumberGenerator.NextDouble() * Sum;
            var iterator = population.Size - 1;

            if (MinValue == null)
            {
                return Population[iterator];
            }

            var localSum = FitnessList[iterator] - MinValue.Value;
            while (localSum < randomValue && iterator > 0)
            {
                localSum += FitnessList[--iterator] - MinValue.Value;
            }

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

            GenerateFitnessList();
            if (Population.HeavenPolicy.UseInCrossover && population.Generation > 0)
            {
                ApplyHeavenPolicy();
            }

            Sum = 0;
            foreach (var x in FitnessList)
            {
                if (MinValue != null)
                {
                    Sum += x - MinValue.Value;
                }
            }

            Deprecated = false;
        }

        private void GenerateFitnessList()
        {
            // Population is already sorted from best to worse
            for (var i = 0; i < Population.Size; i++)
            {
                var functionValue =
                    _rankingValueFunc(CompareCriteria.OptimizationMode == EOptimizationMode.Minimize ? i : Population.Size - 1 - i);
                if (!MinValue.HasValue || functionValue < MinValue)
                {
                    MinValue = functionValue;
                }

                FitnessList[i] = functionValue;
            }
        }

        private void ApplyHeavenPolicy()
        {
            for (var i = Population.Size; i < Population.Size + Population.HeavenPolicy.Size; i++)
            {
                var functionValue = Population.HeavenPolicy.Memory[i - Population.Size].Value;
                if (!MinValue.HasValue || functionValue < MinValue)
                {
                    MinValue = functionValue;
                }

                FitnessList[i] = functionValue;
            }
        }
    }
}