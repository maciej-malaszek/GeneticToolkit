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
                Update(population);

            double randomValue = RandomNumberGenerator.NextDouble() * Sum;
            int iterator = 0;
            double localSum = FitnessList[iterator] - MinValue ?? 0;

            while (localSum < randomValue && iterator < FitnessList.Count - 1)
                localSum += FitnessList[++iterator] - MinValue ?? 0;

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
                for (int i = 0; i < Population.Size; i++)
                {
                    double functionValue = Population[i].Value;
                    if (!MinValue.HasValue || functionValue < MinValue)
                        MinValue = functionValue;
                    FitnessList.Add(functionValue);
                }

                if (Population.HeavenPolicy.UseInCrossover && population.Generation > 0)
                    for (int i = 0; i < Population.HeavenPolicy.Size; i++)
                    {
                        double functionValue = Population.HeavenPolicy.Memory[i].Value;
                        if (!MinValue.HasValue || functionValue < MinValue)
                            MinValue = functionValue;
                        FitnessList.Add(functionValue);
                    }

                Sum = FitnessList.Sum(x => x - MinValue ?? 0);
            }
            else
            {
                for (int i = 0; i < Population.Size; i++)
                {
                    double functionValue = Population[i].Value;
                    if (!MaxValue.HasValue || functionValue > MaxValue)
                        MaxValue = functionValue;
                    FitnessList.Add(functionValue);
                }

                if (Population.HeavenPolicy.UseInCrossover && population.Generation > 0)
                    for (int i = 0; i < Population.HeavenPolicy.Size; i++)
                    {
                        double functionValue = Population.HeavenPolicy.Memory[i].Value;
                        if (!MaxValue.HasValue || functionValue > MaxValue)
                            MaxValue = functionValue;
                        FitnessList.Add(functionValue);
                    }

                if (MaxValue.HasValue)
                    for (int i = 0; i < FitnessList.Count; i++)
                        FitnessList[i] = MaxValue.Value / FitnessList[i];
                Sum = FitnessList.Sum(x => x);
            }

            Deprecated = false;
        }
    }
}