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
    /// <typeparam name="TFitness"></typeparam>
    public class RouletteWheel<TFitness> : ISelectionMethod<TFitness> where TFitness : IComparable
    {
        public ICompareCriteria<TFitness> CompareCriteria { get; set; }

        protected IPopulation<TFitness> Population { get; set; } = null;

        protected IList<IIndividual<TFitness>> SortedList = new List<IIndividual<TFitness>>();

        protected IList<TFitness> FitnessList = new List<TFitness>();

        protected uint CurrentGeneration { get; set; }

        protected bool Deprecated { get; set; } = true;

        protected TFitness MinValue { get; set; }

        protected TFitness MaxValue { get; set; }

        protected TFitness Sum { get; set; }

        public IIndividual<TFitness> Select(IPopulation<TFitness> population)
        {
            Deprecated = population != Population || population.Generation != CurrentGeneration;

            if(Deprecated)
            {
                Population = population;
                CompareCriteria = Population.CompareCriteria;

                SortedList = Population.OrderDescending().ToList();

                // Roullete doesn't work with negatives
                Sum = CompareCriteria.FitnessFunction.Abs(CompareCriteria.FitnessFunction.GetValue(SortedList[0].Phenotype));
                FitnessList.Add(Sum);
                MinValue = Sum;
                MaxValue = Sum;

                for(int i = 1; i < SortedList.Count; i++)
                {

                    TFitness functionValue = CompareCriteria.FitnessFunction.Abs(CompareCriteria.FitnessFunction.GetValue(SortedList[i].Phenotype));

                    MinValue = MinValue.CompareTo(functionValue) > 0 ? functionValue : MinValue;
                    MaxValue = MaxValue.CompareTo(functionValue) < 0 ? functionValue : MaxValue;

                    // Sum += ValueOf(Population[i])
                    Sum = CompareCriteria.FitnessFunction.Add(Sum, functionValue);
                    FitnessList.Add(functionValue);
                }
            }

            TFitness randomValue = CompareCriteria.FitnessFunction.Random(Sum);
            int iterator = 0;
            TFitness localSum = FitnessList[iterator];
            IIndividual<TFitness> selected = SortedList[iterator];

            while(localSum.CompareTo(randomValue) < 0 && iterator < SortedList.Count - 1)
            {
                iterator++;
                localSum = CompareCriteria.FitnessFunction.Add(localSum, FitnessList[iterator]);
            }

            return SortedList[iterator];

        }

    }
}
