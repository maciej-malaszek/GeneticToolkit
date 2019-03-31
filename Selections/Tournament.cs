using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils.Data;

namespace GeneticToolkit.Selections
{
    public class Tournament : ISelectionMethod
    {
        public ICompareCriteria CompareCriteria { get; set; }

        public int TournamentSize { get; set; }

        private readonly Random _random = new Random();

        public IIndividual Select(IPopulation population)
        {
            int realSize = Math.Max(Math.Min(population.Size - 1, TournamentSize), 1);
            if (population == null)
                throw new NullReferenceException("Population has not been initialized!");
            if (population.Size < 2)
                throw new NullReferenceException("Population is smaller than 2 individuals and therefore degenerated!");

            IPopulation tournament = new Population(population.FitnessFunction, realSize)
            {
                CompareCriteria = CompareCriteria,
            };
            for (int i = 0; i < realSize; i++)
                tournament[i] = population[_random.Next(population.Size)];
            return tournament.GetBest();
        }

        public Tournament(ICompareCriteria compareCriteria, int tournamentSize)
        {
            CompareCriteria = compareCriteria;
            TournamentSize = tournamentSize;
        }

        public Tournament(IDictionary<string, object> parameters)
        {
            TournamentSize = (int) parameters["Size"];
        }

        public GeneticAlgorithmParameter Serialize()
        {
            Type type = GetType();
            type = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
            return new GeneticAlgorithmParameter(this)
            {
                Params = new Dictionary<string, object>()
                {
                    {"Size", TournamentSize}
                }
            };
        }
    }
}
