using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils.Factories;

namespace GeneticToolkit
{
    public class Population : IPopulation
    {
        protected List<IIndividual> Individuals;

        private IIndividual _best = null;
        private bool _bestIsDeprecated = true;

        public Population(IFitnessFunction fitnessFunction, int size)
        {
            FitnessFunction = fitnessFunction;
            Individuals = new List<IIndividual>(size);
            for(int i = 0; i < size; i++)
                Individuals.Add(null);
        }

        public IIndividual Best
        {
            get {
                if(_bestIsDeprecated)
                    _best = GetBest();
                return _best;
            }
        }

        public int Size => Individuals.Count;

        public uint Generation { get; protected set; } = 0;

        public IIndividual this[int indexer]
        {
            get => Individuals[indexer];
            set => Individuals[indexer] = value;
        }

        public ICompareCriteria CompareCriteria { get; set; }

        public ICrossOverPolicy CrossOverPolicy { get; set; }

        public IFitnessFunction FitnessFunction { get; set; }

        public IHeavenPolicy HeavenPolicy { get; set; }

        public IIncompatibilityPolicy IncompatibilityPolicy { get; set; }

        public IndividualFactoryBase IndividualFactory { get; set; }

        public IMutationPolicy MutationPolicy { set; get; }

        public IPopulationResizePolicy ResizePolicy { set; get; }

        public ISelectionMethod SelectionMethod { get; set; }

        public IDictionary<string, IStatisticUtility> StatisticUtilities { get; set; }

        public IIndividual GetBest()
        {
            IIndividual best = this[0];
            for(int i = 1; i < Size; i++)
                best = CompareCriteria.GetBetter(best, this[i]);
            return best;
        }

        public IOrderedEnumerable<IIndividual> OrderAscending()
        {
            return Individuals.OrderBy(x => CompareCriteria.FitnessFunction.GetValue(x));
        }

        public IOrderedEnumerable<IIndividual> OrderDescending()
        {
            return Individuals.OrderByDescending(x => CompareCriteria.FitnessFunction.GetValue(x));
        }

        public virtual void Initialize()
        {
            Generation = 0;
            _bestIsDeprecated = true;
            _best = null;
            foreach (KeyValuePair<string, IStatisticUtility> statisticUtility in StatisticUtilities)
                statisticUtility.Value.Reset();

            Individuals = new List<IIndividual>(IndividualFactory.CreateRandomPopulation(Size));
        }

        public virtual void NextGeneration()
        {
            _bestIsDeprecated = true;
            int nextGenSize = ResizePolicy.NextGenSize(this);
            List<IIndividual> nextGeneration = new List<IIndividual>(nextGenSize);

            for(int i = 0; i < nextGenSize; i++)
            {
                IIndividual[] parents = new IIndividual[CrossOverPolicy.ParentsCount];
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
            foreach(KeyValuePair<string, IStatisticUtility> statisticUtility in StatisticUtilities)
            {
                statisticUtility.Value.UpdateData(this);
            }

            GetBest();
        }

        public IEnumerator<IIndividual> GetEnumerator()
        {
            return Individuals.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Individuals.GetEnumerator();
        }
    }
}
