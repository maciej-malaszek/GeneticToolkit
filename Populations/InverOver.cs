using GeneticToolkit.Genotypes.Collective.Combinatory;
using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils.Data;
using GeneticToolkit.Utils.Extensions;
using GeneticToolkit.Utils.Factories;

using System;

namespace GeneticToolkit.Populations
{
    public class InverOverPopulation : IEvolutionaryPopulation
    {
        private readonly Random _random = new Random();
        private bool _bestIsDeprecated = true;
        private IIndividual _best;
        private bool _populationHomogeneityDeprecated = true;
        private float _populationHomogeneity = -1;

        protected IIndividual[] Individuals;

        public float ChangeParentProbability { get; set; }
        public int Size => Individuals.Length;
        public uint Generation { get; protected set; }
        public float Homogeneity
        {
            get
            {
                if (_populationHomogeneityDeprecated)
                {
                    _populationHomogeneity = GetPopulationHomogeneity(0.99);
                    _populationHomogeneityDeprecated = false;
                }
                return _populationHomogeneity;
            }
        }
        public ICompareCriteria CompareCriteria { get; set; }
        public IFitnessFunction FitnessFunction { get; set; }
        public IHeavenPolicy HeavenPolicy { get; set; }
        public IndividualFactoryBase IndividualFactory { get; set; }
        public ContemptibleDictionary<string, IStatisticUtility> StatisticUtilities { get; set; }

        public IIndividual Best
        {
            get {
                if (_bestIsDeprecated)
                    _best = GetBest();
                return _best;
            }
        }

        public InverOverPopulation(IFitnessFunction fitnessFunction, float changeParentProbability, int size)
        {
            FitnessFunction = fitnessFunction;
            ChangeParentProbability = changeParentProbability;
            Individuals = new IIndividual[size];
        }

        public IIndividual this[int indexer]
        {
            get => Individuals[indexer];
            set => Individuals[indexer] = value;
        }

        public void Initialize()
        {
            Generation = 0;
            _bestIsDeprecated = true;
            _best = null;
            foreach (IStatisticUtility statisticUtility in StatisticUtilities.GetValues())
                statisticUtility.Reset();
               
            Individuals = IndividualFactory.CreateRandomPopulation(Size);
            SortDescending();
        }

        public IIndividual GetBest()
        {
            IIndividual best = this[0];
            for (int i = 1; i < Size; i++)
                best = CompareCriteria.GetBetter(best, this[i]);
            return best;
        }

        public void NextGeneration()
        {
            int genotypeLength = (Individuals[0].Genotype as CombinatoryGenotype).Count;
            for(int individualIndex = 0; individualIndex < Size; individualIndex++)
            {
                IIndividual firstIndividual = Individuals[individualIndex];
                var firstParent = (firstIndividual.Genotype as CombinatoryGenotype);
                var child = new short[genotypeLength];
                for (int i = 0; i < genotypeLength; i++)
                    child[i] = firstParent.Value[i];

                int index0 = _random.Next(genotypeLength - 2);
                do
                {
                    int index1;
                    if(_random.NextDouble() <= ChangeParentProbability) 
                        index1=GetEndIndexFromSameParent(index0, genotypeLength);
                    else
                    {
                        index1 = GetEndIndexFromOtherParent(firstParent, index0);
                    }

                    index1 = --index1 >= 0 ? index1 : genotypeLength-1;
                    FixIndexOrder(ref index0, ref index1);
                    if (SelectedNeighbours(index0,index1))
                        break;

                    ReverseSubArray(child, index0, index1);

                    index0 = index1;
                } while (true);

                UpdateIndividual(individualIndex, child);
            }
            
            Generation++;
            SortDescending();
            HeavenPolicy.HandleGeneration(this);
            foreach (IStatisticUtility statisticUtility in StatisticUtilities.GetValues())
                statisticUtility.UpdateData(this);
        }

        public float GetPopulationHomogeneity(double maxSimilarity)
        {
            float similar = 0;
            for (int i = 0; i < Size-1; i++)
            {
                if (this[i].Genotype.SimilarityCheck(this[i + 1].Genotype) > maxSimilarity)
                    similar++;
            }

            return similar / Size;
        }

        public IIndividual[] OrderAscending()
        {
            var sortedList = new IIndividual[Size];
            for (int i = 0; i < Size; i++)
                sortedList[i] = Individuals[i];
            Array.Sort(sortedList,(x1, x2) => CompareCriteria.Compare(x1, x2));
            return sortedList;
        }

        public IIndividual[] OrderDescending()
        {
            var sortedList = new IIndividual[Size];
            for (int i = 0; i < Size; i++)
                sortedList[i] = Individuals[i];
            Array.Sort(sortedList,(x1, x2) => CompareCriteria.Compare(x2, x1));
            return sortedList;
        }

        public void SortAscending()
        {
            Array.Sort(Individuals,(x1, x2) => CompareCriteria.Compare(x1, x2));
        }

        public void SortDescending()
        {
            Array.Sort(Individuals,(x1, x2) => CompareCriteria.Compare(x2, x1));
        }

        private bool SelectedNeighbours(int index0, int index1)
        {
            return index1 - index0 > 1;
        }

        private int GetEndIndexFromSameParent(int index0, int genotypeLength)
        {
            return _random.Next(index0+1, genotypeLength);
        }

        private int GetEndIndexFromOtherParent(CombinatoryGenotype firstParent, int index0)
        {
            var secondParent = Individuals[_random.Next(Size)].Genotype as CombinatoryGenotype;
            int index1 = secondParent.GetIndex(firstParent.Value[index0]) + 1;
            return index1 == secondParent.Count ? 0 : index1;
        }

        private void UpdateIndividual(int index, short[] child)
        {
            var childGenotype = Individuals[index].Genotype.EmptyCopy<CombinatoryGenotype>();
            childGenotype.Value = child;
            IIndividual childIndividual = IndividualFactory.CreateFromGenotype(childGenotype);

            Individuals[index] = CompareCriteria.GetBetter(Individuals[index], childIndividual);
        }

        private void ReverseSubArray(short[] array, int startIndex, int endIndex)
        {
            while (endIndex - startIndex > 0)
                array.Swap(startIndex++, endIndex--);
        }

        private void FixIndexOrder(ref int index0, ref int index1)
        {
            if (index0 <= index1) 
                return;
            var t = index0;
            index0 = index1;
            index1 = t;
        }

    }
}
