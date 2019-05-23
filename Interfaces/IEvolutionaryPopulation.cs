using GeneticToolkit.Utils.Data;
using GeneticToolkit.Utils.Factories;

namespace GeneticToolkit.Interfaces
{
    public interface IEvolutionaryPopulation
    {
        int Size { get; }

        uint Generation { get; }

        float Homogeneity { get; }

        IIndividual this[int indexer] { get; set; }

        ICompareCriteria CompareCriteria { get; set; }

        IFitnessFunction FitnessFunction { get; set; }

        IHeavenPolicy HeavenPolicy { get; set; }

        IndividualFactoryBase IndividualFactory { get; set; }

        ContemptibleDictionary<string, IStatisticUtility> StatisticUtilities { get; set; }

        IIndividual Best { get; }
        IIndividual GetBest();

        IIndividual[] OrderDescending();

        IIndividual[] OrderAscending();

        void SortDescending();

        void SortAscending();

        void Initialize();
        
        void NextGeneration();

        float GetPopulationHomogeneity(double maxSimilarity);
    }
}
