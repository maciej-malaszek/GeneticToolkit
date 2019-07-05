using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace GeneticToolkit.Populations
{
    [PublicAPI]
    public class Population : IPopulation
    {
        private IIndividual _best;
        private bool _bestIsDeprecated = true;
        private bool _populationHomogeneityDeprecated = true;
        private float _populationHomogeneity = -1;
        protected IIndividual[] Individuals;

        public Population(IFitnessFunction fitnessFunction, int size)
        {
            FitnessFunction = fitnessFunction;
            Individuals = new IIndividual[size];
        }

        public int Size => Individuals.Length;
        public uint Generation { get; protected set; }

        public IIndividual Best
        {
            get
            {
                if (_bestIsDeprecated)
                    _best = GetBest();
                return _best;
            }
        }

        public float Homogeneity
        {
            get
            {
                if (!_populationHomogeneityDeprecated)
                    return _populationHomogeneity;
                _populationHomogeneity = GetPopulationHomogeneity(IncestLimit);
                _populationHomogeneityDeprecated = false;
                return _populationHomogeneity;
            }
        }

        public IIndividual this[int indexer]
        {
            get => Individuals[indexer];
            set => Individuals[indexer] = value;
        }

        #region Utils

        public Dictionary<string, IStatisticUtility> StatisticUtilities { get; set; }

        #endregion


        public virtual void Initialize()
        {
            Generation = 0;
            _bestIsDeprecated = true;
            _best = null;
            foreach (var statisticUtility in StatisticUtilities.Values)
                statisticUtility.Reset();

            Individuals = IndividualFactory.CreateRandomPopulation(Size);
            SortDescending();
        }

        public virtual void NextGeneration()
        {
            int nextGenSize = ResizePolicy.NextGenSize(this);
            var nextGeneration = new IIndividual[nextGenSize];
            for (var i = 0; i < nextGenSize / Crossover.ChildrenCount; i++)
            {
                var parentalGenotypes = SelectParentalGenotypes();
                var genotypes = Crossover.Cross(parentalGenotypes);
                for (var j = 0; j < genotypes.Length; j++)
                {
                    var child = IndividualFactory.CreateFromGenotype(genotypes[j]);
                    Mutation.Mutate(child.Genotype, MutationPolicy, this);
                    if (IncompatibilityPolicy.IsCompatible(this, child) == false)
                        child = IncompatibilityPolicy.GetReplacement(this, child, parentalGenotypes);
                    nextGeneration[i * Crossover.ChildrenCount + j] = child;
                }
            }

            int childrenCountDifference = nextGenSize % Crossover.ChildrenCount;
            if (childrenCountDifference > 0)
            {
                var parentalGenotypes = SelectParentalGenotypes();
                var genotypes = Crossover.Cross(parentalGenotypes);
                int start = nextGenSize - childrenCountDifference;
                for (var j = 0; j < childrenCountDifference; j++)
                {
                    var child = IndividualFactory.CreateFromGenotype(genotypes[j]);
                    Mutation.Mutate(child.Genotype, MutationPolicy, this);
                    if (IncompatibilityPolicy.IsCompatible(this, child) == false)
                        child = IncompatibilityPolicy.GetReplacement(this, child, parentalGenotypes);
                    nextGeneration[start + j] = child;
                }
            }


            Individuals = nextGeneration;
            DeprecateData();
            UpdatePerGenerationData();
            if (Generation % 10 == 0)
                GC.Collect();
        }

        public float GetPopulationHomogeneity(double maxSimilarity)
        {
            float similar = 0;
            for (var i = 0; i < Size - 1; i++)
                if (this[i].Genotype.SimilarityCheck(this[i + 1].Genotype) > maxSimilarity)
                    similar++;

            return similar / Size;
        }

        public IIndividual GetBest()
        {
            var best = this[0];
            for (var i = 1; i < Size; i++)
                best = CompareCriteria.GetBetter(best, this[i]);
            return best;
        }

        #region Params

        public int MaxSelectionTries { get; set; } = 10;
        public float IncestLimit { get; set; } = 0.99f;
        public float DegenerationLimit { get; set; } = 0.95f;
        public ICompareCriteria CompareCriteria { get; set; }
        public ICrossover Crossover { get; set; }
        public IMutation Mutation { get; set; }
        public IFitnessFunction FitnessFunction { get; set; }
        public IHeavenPolicy HeavenPolicy { get; set; }
        public IIncompatibilityPolicy IncompatibilityPolicy { get; set; }
        public IndividualFactoryBase IndividualFactory { get; set; }
        public IMutationPolicy MutationPolicy { set; get; }
        public IPopulationResizePolicy ResizePolicy { set; get; }
        public ISelectionMethod SelectionMethod { get; set; }

        #endregion

        #region Private Methods

        private bool ParentAlreadySelected(IGenotype candidate, IEnumerable<IGenotype> parents)
        {
            return parents.Any(parent =>
                parent != null && (candidate == parent || candidate.SimilarityCheck(parent) > IncestLimit));
        }

        private IGenotype[] SelectParentalGenotypes()
        {
            var parents = new IGenotype[Crossover.ParentsCount];
            for (var x = 0; x < parents.Length; x++)
            {
                var trial = 0;
                var candidate = SelectionMethod.Select(this).Genotype;
                while (ParentAlreadySelected(candidate, parents) && Homogeneity < DegenerationLimit &&
                       trial++ < MaxSelectionTries)
                    candidate = SelectionMethod.Select(this).Genotype;
                parents[x] = candidate;
            }

            return parents;
        }

        private void DeprecateData()
        {
            _bestIsDeprecated = true;
            _populationHomogeneityDeprecated = true;
        }

        private void UpdatePerGenerationData()
        {
            Generation++;
            HeavenPolicy.HandleGeneration(this);
            foreach (var statisticUtility in StatisticUtilities.Values)
                statisticUtility.UpdateData(this);
        }

        #endregion

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
            Array.Sort(Individuals, (x1, x2) => CompareCriteria.Compare(x1, x2));
        }

        public void SortDescending()
        {
            Array.Sort(Individuals, (x1, x2) => CompareCriteria.Compare(x2, x1));
        }

        #endregion
    }
}