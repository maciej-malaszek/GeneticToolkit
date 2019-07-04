using GeneticToolkit.Interfaces;
using GeneticToolkit.Populations;
using System;
using JetBrains.Annotations;

namespace GeneticToolkit.Selections
{
    [PublicAPI]
    public class Tournament : ISelectionMethod
    {
        private readonly Random _random = new Random();
        public Tournament(ICompareCriteria compareCriteria, float populationPercentage)
        {
            CompareCriteria = compareCriteria;
            PopulationPercentage = populationPercentage;
        }
        public float PopulationPercentage { get; set; }
        public ICompareCriteria CompareCriteria { get; set; }
        public IIndividual Select(IPopulation population)
        {
            int realSize = Math.Max(Math.Min(population.Size - 1, (int) (PopulationPercentage * population.Size)), 1);
            if (population == null)
                throw new NullReferenceException("Population has not been initialized!");
            if (population.Size < 2)
                throw new NullReferenceException("Population is smaller than 2 individuals and therefore degenerated!");

            IPopulation tournament = new Population(population.FitnessFunction, realSize)
            {
                CompareCriteria = CompareCriteria,
            };
            for (var i = 0; i < realSize; i++)
                tournament[i] = population[_random.Next(population.Size)];
            return tournament.GetBest();
        }
    }
}