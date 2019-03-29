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

        public Population(IFitnessFunction fitnessFunction, int size)
        {
            FitnessFunction = fitnessFunction;
            Individuals = new List<IIndividual>(size);
            for(int i = 0; i < size; i++)
                Individuals.Add(null);
        }
        public IIndividual this[int indexer]
        {
            get => Individuals[indexer];
            set => Individuals[indexer] = value;
        }

        #region Params
            public ICompareCriteria CompareCriteria { get; set; }

            public ICrossover Crossover { get; set; }

            public IFitnessFunction FitnessFunction { get; set; }

            public IHeavenPolicy HeavenPolicy { get; set; }

            public IIncompatibilityPolicy IncompatibilityPolicy { get; set; }

            public IndividualFactoryBase IndividualFactory { get; set; }

            public IMutationPolicy MutationPolicy { set; get; }

            public IPopulationResizePolicy ResizePolicy { set; get; }

            public ISelectionMethod SelectionMethod { get; set; }

        #endregion

        #region Utils
            public IDictionary<string, IStatisticUtility> StatisticUtilities { get; set; }
        #endregion

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
                var parents = new IIndividual[Crossover.ParentsCount];
                for(int x = 0; x < parents.Length; x++)
                    parents[x] = SelectionMethod.Select(this);

                var genotypes =  Crossover.Cross(parents.Select(x => x.Genotype).ToList());
                foreach (IGenotype genotype in genotypes)
                {
                    IIndividual child = IndividualFactory.CreateFromGenotype(genotype, parents[0].Phenotype.ShallowCopy());
                    child.Mutate(MutationPolicy);
                    nextGeneration.Add(child);
                }

                if(IncompatibilityPolicy.IsCompatible(this, nextGeneration[i]) == false)
                {
                    nextGeneration[i] = IncompatibilityPolicy.GetReplacement(this, nextGeneration[i], parents);
                    if(nextGeneration[i] == null)
                    {
                        Individuals.RemoveAt(i);
                        i--;
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

        public IIndividual GetBest()
        {
            IIndividual best = this[0];
            for(int i = 1; i < Size; i++)
                best = CompareCriteria.GetBetter(best, this[i]);
            return best;
        }

        #region Tools
            public List<IIndividual> OrderAscending()
            {
                List<IIndividual> sortedList = Individuals;
                sortedList.Sort((x1, x2) => CompareCriteria.Compare(x1, x2));
                return sortedList;
            }

            public List<IIndividual> OrderDescending()
            {
                List<IIndividual> sortedList = Individuals;
                sortedList.Sort((x1, x2) => CompareCriteria.Compare(x2, x1));
                return sortedList;
            }

            public IEnumerator<IIndividual> GetEnumerator()
            {
                return Individuals.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return Individuals.GetEnumerator();
            }

        #endregion
    }
}
