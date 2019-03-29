using System.Collections.Generic;
using System.Linq;
using GeneticToolkit.Utils.Factories;

namespace GeneticToolkit.Interfaces
{
    public interface IPopulation : IEnumerable<IIndividual>
    {
        int Size { get; }

        uint Generation { get; }

        IIndividual this[int indexer] { get; set; }

        ICompareCriteria CompareCriteria { get; set; }

        ICrossover Crossover { get; set; }

        IFitnessFunction FitnessFunction { get; set; }

        IHeavenPolicy HeavenPolicy { get; set; }

        IIncompatibilityPolicy IncompatibilityPolicy { get; set; }

        IndividualFactoryBase IndividualFactory { get; set; }

        IMutationPolicy MutationPolicy { set; get; }

        IPopulationResizePolicy ResizePolicy { set; get; }

        ISelectionMethod SelectionMethod { get; set; }

        IDictionary<string, IStatisticUtility> StatisticUtilities { get; set; }

        IIndividual GetBest();

        List<IIndividual> OrderDescending();

        List<IIndividual> OrderAscending();

        void Initialize();

        void NextGeneration();

    }
}
