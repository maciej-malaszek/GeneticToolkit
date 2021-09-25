using GeneticToolkit.Interfaces;
using GeneticToolkit.Populations;

using JetBrains.Annotations;

using System;
using System.Linq;
using GeneticToolkit.Utils.Exceptions;

namespace GeneticToolkit.Selections
{
    [PublicAPI]
    public class Tournament<TFitnessFunctionFactory> : ISelectionMethod where TFitnessFunctionFactory : IFitnessFunctionFactory, new()
    {
        private static readonly Random _randomNumberGenerator = new();

        public Tournament()
        {
            
        }
        public Tournament(ICompareCriteria compareCriteria, float populationPercentage)
        {
            CompareCriteria = compareCriteria;
            PopulationPercentage = populationPercentage;
        }
        public float PopulationPercentage { get; set; }
        public ICompareCriteria CompareCriteria { get; set; }

        public IIndividual Select(IPopulation population)
        {
            var realSize = Math.Max(Math.Min(population.Size - 1, Math.Max(2,(int)(PopulationPercentage * population.Size))), 1);
            if (population.Size < 2)
            {
                throw new PopulationTooSmallException(population.Size, 2);
            }

            var tournament = new Population<TFitnessFunctionFactory>(realSize)
            {
                CompareCriteria = CompareCriteria,
            };
            
            for (var i = 0; i < realSize; i++)
            {
                if (population.HeavenPolicy.UseInCrossover == false)
                {
                    tournament[i] = population[_randomNumberGenerator.Next(population.Size)];
                }
                else
                {
                    var index = _randomNumberGenerator.Next(population.Size + population.HeavenPolicy.Size);
                    if (index < population.Size)
                    {
                        tournament[i] = population[index];
                    }
                    else
                    {
                        tournament[i] = population.HeavenPolicy.Memory[index - population.Size];
                    }
                }
            }
            return tournament.GetBest();
        }

    }
}