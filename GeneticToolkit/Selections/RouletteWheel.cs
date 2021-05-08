using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils.Data;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticToolkit.Selections
{
    /// <summary>
    /// Selection method that assures best individuals have highest chance of selection.
    /// One of slowest, because of more arithmetic operations
    /// </summary>
    [PublicAPI]
    public class RouletteWheel : ISelectionMethod
    {
        public ICompareCriteria CompareCriteria { get; set; }

        protected static Random RandomNumberGenerator { get; set; } = new Random();
        protected IPopulation Population { get; set; }

        protected List<double> FitnessList = new List<double>();
        protected uint CurrentGeneration { get; set; }
        protected bool Deprecated { get; set; } = true;

        protected double? MinValue { get; set; }
        protected double? MaxValue { get; set; }
        protected double Sum { get; set; }


        public IIndividual Select(IPopulation population)
        {
            Deprecated = population != Population || population.Generation != CurrentGeneration;
            if (Deprecated)
            {
                Update(population);
            }

            var randomValue = RandomNumberGenerator.NextDouble() * Sum;
            var iterator = -1;
            var localSum = 0.0;
            do
            {
                localSum += FitnessList[++iterator] - MinValue ?? 0;
            } while (localSum < randomValue && iterator < FitnessList.Count - 1);



            return iterator < population.Size
                ? Population[iterator]
                : Population.HeavenPolicy.Memory[iterator - population.Size];
        }

        private void Update(IPopulation population)
        {
            Population = population;
            CompareCriteria = Population.CompareCriteria;
            CurrentGeneration = population.Generation;
            MinValue = null;
            MaxValue = null;
            FitnessList.Clear();
            if (CompareCriteria.OptimizationMode == EOptimizationMode.Maximize)
            {
                MaximizeValue(population);
            }
            else
            {
                MinimizeValue(population);
            }

            Deprecated = false;
        }

        private void MinimizeValue(IPopulation population)
        {
            for (var i = 0; i < Population.Size; i++)
            {
                var functionValue = Population[i].Value;
                if (!MaxValue.HasValue || functionValue > MaxValue)
                {
                    MaxValue = functionValue;
                }

                FitnessList.Add(functionValue);
            }

            ApplyHeavenPolicy(population, EOptimizationMode.Maximize);

            if (MaxValue.HasValue)
            {
                for (var i = 0; i < FitnessList.Count; i++)
                {
                    // Reverse values, so roulette could give highest chances to smallest values
                    FitnessList[i] = MaxValue.Value / FitnessList[i]; 
                }
            }

            Sum = FitnessList.Sum(x => x);
        }

        private void MaximizeValue(IPopulation population)
        {
            for (var i = 0; i < Population.Size; i++)
            {
                var functionValue = Population[i].Value;
                if (!MinValue.HasValue || functionValue < MinValue)
                {
                    MinValue = functionValue;
                }

                FitnessList.Add(functionValue);
            }

            ApplyHeavenPolicy(population, EOptimizationMode.Maximize);

            Sum = FitnessList.Sum(x => x - MinValue ?? 0);
        }

        private void ApplyHeavenPolicy(IPopulation population, EOptimizationMode optimizationMode)
        {
            if (!Population.HeavenPolicy.UseInCrossover || population.Generation <= 0)
            {
                return;
            }

            for (var i = 0; i < Population.HeavenPolicy.Size; i++)
            {
                var functionValue = UpdateBestValueOf(optimizationMode, i);
                FitnessList.Add(functionValue);
            }
        }

        private double UpdateBestValueOf(EOptimizationMode optimizationMode, int i)
        {
            var functionValue = Population.HeavenPolicy.Memory[i].Value;
            if (optimizationMode == EOptimizationMode.Maximize)
            {
                MinValue ??= functionValue;
                if (functionValue < MinValue)
                {
                    MinValue = functionValue;
                }
            }
            else
            {
                MaxValue ??= functionValue;
                if (functionValue > MaxValue)
                {
                    MaxValue = functionValue;
                }
            }

            return functionValue;
        }
    }
}