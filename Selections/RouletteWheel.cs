using System;
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
        protected Random RandomNumberGenerator { get; set; } = new Random();
        public ICompareCriteria CompareCriteria { get; set; }

        protected IPopulation Population { get; set; } = null;

        protected IList<IIndividual> SortedList = new List<IIndividual>();

        protected IList<double> FitnessList = new List<double>();

        protected uint CurrentGeneration { get; set; }

        protected bool Deprecated { get; set; } = true;

        protected double MinValue { get; set; }

        protected double MaxValue { get; set; }

        protected double Sum { get; set; } = 0;

        public IIndividual Select(IPopulation population)
        {
            Deprecated = population != Population || population.Generation != CurrentGeneration;

            if(Deprecated)
                Update(population);

            double randomValue = RandomNumberGenerator.NextDouble() * Sum;
            int iterator = -1;
            double localSum = 0;

            do { localSum += FitnessList[++iterator]; }
            while(localSum < randomValue && iterator < SortedList.Count);

            return SortedList[iterator];

        }

        private void Update(IPopulation population)
        {
            Population = population;
            CompareCriteria = Population.CompareCriteria;

            SortedList = Population.OrderDescending().ToList();

            // Roullete doesn't work with negatives

            MinValue = 0;
            MaxValue = 0;

            foreach(IIndividual ind in SortedList)
            {
                double functionValue = Math.Abs(CompareCriteria.FitnessFunction.GetValue(ind.Phenotype));

                if(functionValue < MinValue)
                    MinValue = functionValue;

                if(functionValue > MaxValue)
                    MaxValue = functionValue;

                Sum += functionValue;
                FitnessList.Add(functionValue);
            }
        }

    }
}
