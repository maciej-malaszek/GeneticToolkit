using System;
using System.Collections.Generic;
using GeneticToolkit.Factories;
using JetBrains.Annotations;

namespace GeneticToolkit.Interfaces
{
    [PublicAPI]
    public interface IEvolutionaryPopulation : IEnumerable<IIndividual>
    {
        int Size { get; }

        uint Generation { get; }

        float Homogeneity { get; }

        IIndividual this[int indexer] { get; set; }

        ICompareCriteria CompareCriteria { get; set; }

        IFitnessFunction FitnessFunction { get; }

        IHeavenPolicy HeavenPolicy { get; set; }

        IndividualFactoryBase IndividualFactory { get; set; }

        Dictionary<string, IStatisticUtility> StatisticUtilities { get; set; }

        IIndividual Best { get; }
        
        IIndividual GetBest();

        IIndividual[] OrderDescending();

        IIndividual[] OrderAscending();

        void SortDescending();

        void SortAscending();

        void Initialize();

        void Initialize(Func<IIndividual[]> populationGenerator);
        
        void NextGeneration();

        float GetPopulationHomogeneity(double maxSimilarity);
    }
}
