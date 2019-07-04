using GeneticToolkit.Interfaces;

using System;

namespace GeneticToolkit.Selections
{
    public class RankRoulette : ISelectionMethod
    {
        public ICompareCriteria CompareCriteria { get; set; }

        public Func<int, double> RankingValueFunc;

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

            if(Deprecated)
                Update(population);

            double randomValue = RandomNumberGenerator.NextDouble() * Sum;
            int iterator = population.Size-1;

            if (MinValue == null) 
                return Population[iterator];

            double localSum = FitnessList[iterator] - MinValue.Value;
            while(localSum < randomValue && iterator > 0)
                localSum += FitnessList[--iterator] - MinValue.Value;

            return Population[iterator];
        }

        public void Update(IPopulation population)
        {
            Population = population;
            Population.SortDescending();
            CompareCriteria = Population.CompareCriteria;
            CurrentGeneration = Population.Generation;
            FitnessList = new double[Population.Size];
            MinValue = null;

            // Population is already sorted from best to worse
            for(int i = 0; i < Population.Size; i++)
            {
                double functionValue = RankingValueFunc(i);
                if(functionValue < MinValue || MinValue == null)
                    MinValue = functionValue;
                FitnessList[i] = functionValue;
            }

            Sum = 0;
            foreach (var x in FitnessList)
                if (MinValue != null)
                    Sum += x - MinValue.Value;
            Deprecated = false;
        }
    }
}
