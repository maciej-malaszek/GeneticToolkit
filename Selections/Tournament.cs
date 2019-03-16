using System;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Selections
{
    public class Tournament<TFitness> : ISelectionMethod<TFitness> where TFitness:IComparable
    {
        public ICompareCriteria<TFitness> CompareCriteria { get; set; }
        
        public int TournamentSize { get; set; }

        private readonly Random _random = new Random();

        public IIndividual<TFitness> Select(IPopulation<TFitness> population)
        {
            int realSize = Math.Max(Math.Min(population.Size - 1, TournamentSize), 1);
            if(population == null)
                throw new NullReferenceException("Population has not been initialized!");
            if(population.Size < 2)
                throw new NullReferenceException("Population is smaller than 2 individuals and therefore degenerated!");

            IPopulation<TFitness> tournament = new Population<TFitness>(CompareCriteria.FitnessFunction, realSize)
            {
                CompareCriteria = this.CompareCriteria,
                
            };
            for(int i = 0; i < realSize; i++)
                tournament[i] = population[_random.Next(population.Size)];
            return tournament.GetBest();
        }

        public Tournament(ICompareCriteria<TFitness> compareCriteria, int tournamentSize)
        {
            CompareCriteria = compareCriteria;
            TournamentSize = tournamentSize;
        }
    }
}
