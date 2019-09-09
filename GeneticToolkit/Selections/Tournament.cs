using GeneticToolkit.Interfaces;
using GeneticToolkit.Populations;

using JetBrains.Annotations;

using System;

namespace GeneticToolkit.Selections
{
    [PublicAPI]
    public class Tournament : ISelectionMethod
    {
        private static readonly Random RandomNumberGenerator = new Random();

        public Tournament(ICompareCriteria compareCriteria, float populationPercentage)
        {
            CompareCriteria = compareCriteria;
            PopulationPercentage = populationPercentage;
        }
        public float PopulationPercentage { get; set; }
        public ICompareCriteria CompareCriteria { get; set; }

        public IIndividual Select(IPopulation population)
        {
            int realSize = Math.Max(Math.Min(population.Size - 1, Math.Max(2,(int)(PopulationPercentage * population.Size))), 1);
            if (population == null)
                throw new NullReferenceException("Population has not been initialized!");
            if (population.Size < 2)
                throw new NullReferenceException("Population is smaller than 2 individuals and therefore degenerated!");

            var tournament = new Population(population.FitnessFunction, realSize)
            {
                CompareCriteria = CompareCriteria,
            };
            for (int i = 0; i < realSize; i++)
            {
                if (population.HeavenPolicy.UseInCrossover)
                {
                    tournament[i] = population[RandomNumberGenerator.Next(population.Size)];
                }
                else
                {
                    int index = RandomNumberGenerator.Next(population.Size + population.HeavenPolicy.Size);
                    if (index < population.Size)
                        tournament[i] = population[index];
                    else tournament[i] = population.HeavenPolicy.Memory[index - population.Size];
                }
            }
            return tournament.GetBest();
        }

    }
}