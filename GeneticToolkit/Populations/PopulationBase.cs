using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils.Factories;

namespace GeneticToolkit.Populations
{
    public abstract class PopulationBase : IEvolutionaryPopulation
    {
        protected readonly Random Random = new Random();
        private bool _sorted;
        protected bool BestIsDeprecated = true;
        private IIndividual _best;
        protected bool PopulationHomogeneityDeprecated = true;
        protected float PopulationHomogeneity = -1;

        protected IIndividual[] Individuals;
        IEnumerator<IIndividual> IEnumerable<IIndividual>.GetEnumerator()
        {
            return Individuals.AsEnumerable().GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return Individuals.GetEnumerator();
        }

        public int Size => Individuals.Length;
        public uint Generation { get; protected set; }
        public float IncestLimit { get; set; } = 0.99f;
        public float DegenerationLimit { get; set; } = 0.95f;
        public float Homogeneity
        {
            get
            {
                if (!PopulationHomogeneityDeprecated)
                    return PopulationHomogeneity;
                PopulationHomogeneity = GetPopulationHomogeneity(IncestLimit);
                PopulationHomogeneityDeprecated = false;
                return PopulationHomogeneity;
            }
        }


        public IIndividual this[int indexer]
        {
            get => Individuals[indexer];
            set => Individuals[indexer] = value;
        }

        public ICompareCriteria CompareCriteria { get; set; }
        public IFitnessFunction FitnessFunction { get; set; }
        public IHeavenPolicy HeavenPolicy { get; set; }
        public IndividualFactoryBase IndividualFactory { get; set; }
        #region Utils

        public Dictionary<string, IStatisticUtility> StatisticUtilities { get; set; }

        #endregion
        public IIndividual Best
        {
            get
            {
                if (BestIsDeprecated)
                    _best = GetBest();
                return _best;
            }
        }

        public IIndividual GetBest()
        {
            IIndividual best = this[0];
            for (var i = 1; i < Size; i++)
                best = CompareCriteria.GetBetter(best, this[i]);
            return best;
        }

        #region Tools

        public IIndividual[] OrderAscending()
        {
            var sortedList = new IIndividual[Size];
            for (var i = 0; i < Size; i++)
                sortedList[i] = Individuals[i];
            Array.Sort(sortedList, (x1, x2) => CompareCriteria.Compare(x1, x2));
            return sortedList;
        }

        public IIndividual[] OrderDescending()
        {
            var sortedList = new IIndividual[Size];
            for (var i = 0; i < Size; i++)
                sortedList[i] = Individuals[i];
            Array.Sort(sortedList, (x1, x2) => CompareCriteria.Compare(x2, x1));
            return sortedList;
        }

        public void SortAscending()
        {
            if(_sorted)
                return;
            Array.Sort(Individuals, (x1, x2) => CompareCriteria.Compare(x1, x2));
            _sorted = true;
        }

        public void SortDescending()
        {
            if(_sorted)
                return;
            Array.Sort(Individuals, (x1, x2) => CompareCriteria.Compare(x2, x1));
            _sorted = true;
        }

        #endregion
        
        public void Reset()
        {
            Generation = 0;
            BestIsDeprecated = true;
            _best = null;
            foreach (IStatisticUtility statisticUtility in StatisticUtilities.Values)
                statisticUtility.Reset();
        }

        public virtual void Initialize()
        {
            Reset();
            Individuals = IndividualFactory.CreateRandomPopulation(Size);
            SortDescending();
        }

        public virtual void Initialize(Func<IIndividual[]> populationGenerator)
        {
            Reset();
            Individuals = populationGenerator();
            SortDescending();
        }

        public abstract void NextGeneration();

        public float GetPopulationHomogeneity(double maxSimilarity)
        {
            SortDescending();
            float similar = 0;
            for (var i = 0; i < Size - 1; i++)
                if (this[i].Genotype.SimilarityCheck(this[i + 1].Genotype) > maxSimilarity)
                    similar++;

            return similar / Size;
        }
        
        protected void DeprecateData()
        {
            BestIsDeprecated = true;
            PopulationHomogeneityDeprecated = true;
        }

        protected void UpdatePerGenerationData()
        {
            Generation++;
            HeavenPolicy.HandleGeneration(this);
            foreach (IStatisticUtility statisticUtility in StatisticUtilities.Values)
                statisticUtility.UpdateData(this);
        }
    }
}