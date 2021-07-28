using GeneticToolkit.Genotypes.Collective.Combinatory;
using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils.Extensions;

namespace GeneticToolkit.Populations
{
    public class InverOverPopulation<TFitnessFunctionFactory> : PopulationBase<TFitnessFunctionFactory>
        where TFitnessFunctionFactory : IFitnessFunctionFactory, new()
    {
        public float ChangeParentProbability { get; set; }

        public InverOverPopulation()
        {
        }

        public InverOverPopulation(float changeParentProbability, int size)
        {
            ChangeParentProbability = changeParentProbability;
            Individuals = new IIndividual[size];
        }

        public override void NextGeneration()
        {
            var genotypeLength = ((CombinatoryGenotype) Individuals[0].Genotype).Count;
            for (var individualIndex = 0; individualIndex < Size; individualIndex++)
            {
                var firstIndividual = Individuals[individualIndex];
                var firstParent = (CombinatoryGenotype) firstIndividual.Genotype;
                var child = new short[genotypeLength];
                for (var i = 0; i < genotypeLength; i++)
                {
                    child[i] = firstParent.Value[i];
                }

                var index0 = Random.Next(genotypeLength - 2);
                do
                {
                    var index1 = Random.NextDouble() <= ChangeParentProbability
                        ? GetEndIndexFromSameParent(index0, genotypeLength)
                        : GetEndIndexFromOtherParent(firstParent, index0);

                    index1 = --index1 >= 0 ? index1 : genotypeLength - 1;
                    FixIndexOrder(ref index0, ref index1);
                    if (SelectedNeighbours(index0, index1))
                    {
                        break;
                    }

                    ReverseSubArray(child, index0, index1);

                    index0 = index1;
                } while (true);

                UpdateIndividual(individualIndex, child);
            }

            DeprecateData();
            UpdatePerGenerationData();
        }

        private static bool SelectedNeighbours(int index0, int index1)
        {
            return index1 - index0 > 1;
        }

        private int GetEndIndexFromSameParent(int index0, int genotypeLength)
        {
            return Random.Next(index0 + 1, genotypeLength);
        }

        private int GetEndIndexFromOtherParent(CombinatoryGenotype firstParent, int index0)
        {
            var secondParent = (CombinatoryGenotype) Individuals[Random.Next(Size)].Genotype;
            var index1 = secondParent.GetIndex(firstParent.Value[index0]) + 1;
            return index1 == secondParent.Count ? 0 : index1;
        }

        private void UpdateIndividual(int index, short[] child)
        {
            var childGenotype = Individuals[index].Genotype.EmptyCopy<CombinatoryGenotype>();
            childGenotype.Value = child;
            var childIndividual = IndividualFactory.CreateFromGenotype(childGenotype);
            Individuals[index] = CompareCriteria.GetBetter(Individuals[index], childIndividual);
        }

        private static void ReverseSubArray(short[] array, int startIndex, int endIndex)
        {
            while (endIndex - startIndex > 0)
            {
                array.Swap(startIndex++, endIndex--);
            }
        }

        private static void FixIndexOrder(ref int index0, ref int index1)
        {
            if (index0 <= index1)
            {
                return;
            }

            var t = index0;
            index0 = index1;
            index1 = t;
        }
    }
}