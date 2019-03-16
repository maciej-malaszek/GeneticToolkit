using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit
{
    public class Population<TFitness> : IPopulation<TFitness> where TFitness : IComparable
    {
        protected List<IIndividual<TFitness>> Individuals;

        private IIndividual<TFitness> _best = null;
        private bool _bestIsDeprecated = true;

        public Population(IFitnessFunction<TFitness> fitnessFunction, int size)
        {
            FitnessFunction = fitnessFunction;
            Individuals = new List<IIndividual<TFitness>>(size);
            for(int i = 0; i < size; i++)
                Individuals.Add(null);
        }

        public IIndividual<TFitness> Best
        {
            get {
                if(_bestIsDeprecated)
                    _best = GetBest();
                return _best;
            }
        }

        public int Size => Individuals.Count;

        public uint Generation { get; protected set; } = 0;

        public IIndividual<TFitness> this[int indexer]
        {
            get => Individuals[indexer];
            set => Individuals[indexer] = value;
        }

        public ICompareCriteria<TFitness> CompareCriteria { get; set; }

        public ICrossOverPolicy<TFitness> CrossOverPolicy { get; set; }

        public IFitnessFunction<TFitness> FitnessFunction { get; set; }

        public IHeavenPolicy<TFitness> HeavenPolicy { get; set; }

        public IIncompatibilityPolicy<TFitness> IncompatibilityPolicy { get; set; }

        public IIndividualFactory<TFitness> IndividualFactory { get; set; }

        public IMutationPolicy<TFitness> MutationPolicy { set; get; }

        public IPopulationResizePolicy<TFitness> ResizePolicy { set; get; }

        public ISelectionMethod<TFitness> SelectionMethod { get; set; }

        public IDictionary<string, IStatisticUtility<TFitness>> StatisticUtilities { get; set; }

        public IIndividual<TFitness> GetBest()
        {
            IIndividual<TFitness> best = this[0];
            for(int i = 1; i < Size; i++)
                best = CompareCriteria.GetBetter(best, this[i]);
            return best;
        }

        public IOrderedEnumerable<IIndividual<TFitness>> OrderAscending()
        {
            return Individuals.OrderBy(x => CompareCriteria.FitnessFunction.GetValue(x));
        }

        public IOrderedEnumerable<IIndividual<TFitness>> OrderDescending()
        {
            return Individuals.OrderByDescending(x => CompareCriteria.FitnessFunction.GetValue(x));
        }

        public virtual void Initialize()
        {
            Individuals = new List<IIndividual<TFitness>>(IndividualFactory.CreateRandomPopulation(Size));
        }

        public virtual void NextGeneration()
        {
            _bestIsDeprecated = true;
            int nextGenSize = ResizePolicy.NextGenSize(this);
            List<IIndividual<TFitness>> nextGeneration = new List<IIndividual<TFitness>>(nextGenSize);

            for(int i = 0; i < nextGenSize; i++)
            {
                IIndividual<TFitness>[] parents = new IIndividual<TFitness>[CrossOverPolicy.ParentsCount];
                for(int x = 0; x < parents.Length; x++)
                    parents[x] = SelectionMethod.Select(this);
                nextGeneration.Add(parents[0].CrossOver(CrossOverPolicy, parents));
                nextGeneration[i].Mutate(MutationPolicy);

                if(IncompatibilityPolicy.IsCompatible(this, nextGeneration[i]) == false)
                {
                    nextGeneration[i] = IncompatibilityPolicy.GetReplacement(this, nextGeneration[i], parents);
                    if(nextGeneration[i] == null)
                    {
                        Individuals.RemoveAt(i);
                        i--;
                        continue;
                    }
                }
            }

            Generation++;
            Individuals = nextGeneration;
            HeavenPolicy.HandleGeneration(this);
            foreach(KeyValuePair<string,IStatisticUtility<TFitness>> statisticUtility in StatisticUtilities)
            {
                statisticUtility.Value.UpdateData(this);
            }

            GetBest();
        }

    }
}
