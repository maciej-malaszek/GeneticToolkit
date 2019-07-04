using GeneticToolkit.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

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

        protected Random RandomNumberGenerator { get; set; } = new Random();

        protected IPopulation Population { get; set; }

        protected List<double> FitnessList = new List<double>();

        protected uint CurrentGeneration { get; set; }

        protected bool Deprecated { get; set; } = true;

        protected double? MinValue { get; set; }

        protected double Sum { get; set; }

        public IIndividual Select(IPopulation population)
        {
            Deprecated = population != Population || population.Generation != CurrentGeneration;
            if (Deprecated)
                Update(population);

            double randomValue = RandomNumberGenerator.NextDouble() * Sum;
            var iterator = 0;
            double localSum = FitnessList[iterator] - MinValue ?? 0;

            while (localSum < randomValue && iterator < Population.Size - 1)
                localSum += FitnessList[++iterator] - MinValue ?? 0;

            return Population[iterator];
        }

        private void Update(IPopulation population)
        {
            Population = population;
            CompareCriteria = Population.CompareCriteria;
            CurrentGeneration = population.Generation;
            MinValue = null;
            FitnessList.Clear();

            for (var i = 0; i < Population.Size; i++)
            {
                double functionValue = Population.FitnessFunction.GetValue(Population[i].Phenotype);
                if (functionValue < MinValue || MinValue.HasValue == false)
                    MinValue = functionValue;
                FitnessList.Add(functionValue);
            }

            Deprecated = false;
            Sum = FitnessList.Sum(x => x - (MinValue ?? 0));
        }
    }
}