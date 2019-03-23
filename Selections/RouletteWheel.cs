﻿using System;
using System.Collections.Generic;
using System.Linq;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Selections
{
    /// <summary>
    /// Selection method that assures best individuals have highest chance of selection.
    /// One of slowest, because of more arithmetic operations
    /// </summary>
    public class RouletteWheel : ISelectionMethod
    {
        public ICompareCriteria CompareCriteria { get; set; }

        protected Random RandomNumberGenerator { get; set; } = new Random();

        protected IPopulation Population { get; set; } = null;

        protected IList<double> FitnessList = new List<double>();

        protected uint CurrentGeneration { get; set; }

        protected bool Deprecated { get; set; } = true;

        protected double? MinValue { get; set; }

        protected double Sum { get; set; } = 0;

        public IIndividual Select(IPopulation population)
        {
            Deprecated = population != Population || population.Generation != CurrentGeneration;
            if(Deprecated)
                Update(population);

            double randomValue = RandomNumberGenerator.NextDouble() * Sum;
            int iterator = 0;
            double localSum = FitnessList[iterator] - MinValue.Value;

            while (localSum < randomValue && iterator < Population.Size-1)
                localSum += FitnessList[++iterator] - MinValue.Value;

            return Population[iterator];

        }

        private void Update(IPopulation population)
        {
            Population = population;
            CompareCriteria = Population.CompareCriteria;
            CurrentGeneration = population.Generation;
            MinValue = null;
            FitnessList.Clear();
            
            foreach(IIndividual ind in Population)
            {
                double functionValue = CompareCriteria.FitnessFunction.GetValue(ind.Phenotype);
                if(functionValue < MinValue || MinValue.HasValue == false)
                    MinValue = functionValue;
                FitnessList.Add(functionValue);
            }
            Deprecated = false;
            Sum = FitnessList.Sum(x => x - MinValue.Value);
            
        }

    }
}
