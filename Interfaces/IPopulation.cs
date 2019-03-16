using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GeneticToolkit.Interfaces
{
    public interface IPopulation<TFitness> where TFitness:IComparable
    {
        int Size { get; }

        uint Generation { get; }

        IIndividual<TFitness>  this[int indexer] { get; set; }

        ICompareCriteria<TFitness> CompareCriteria { get; set; }

        ICrossOverPolicy<TFitness> CrossOverPolicy { get; set; }

        IFitnessFunction<TFitness> FitnessFunction { get; set; }

        IHeavenPolicy<TFitness> HeavenPolicy { get; set; }

        IIncompatibilityPolicy<TFitness> IncompatibilityPolicy { get; set; }

        IIndividualFactory<TFitness> IndividualFactory { get; set; }

        IMutationPolicy<TFitness> MutationPolicy { set; get; }

        IPopulationResizePolicy<TFitness> ResizePolicy { set; get; }

        ISelectionMethod<TFitness> SelectionMethod { get; set; }

        IDictionary<string, IStatisticUtility<TFitness>> StatisticUtilities { get; set; }

        IIndividual<TFitness>  GetBest();

        IOrderedEnumerable<IIndividual<TFitness>> OrderDescending();

        IOrderedEnumerable<IIndividual<TFitness>> OrderAscending();

        void Initialize();

        void NextGeneration();

    }
}
