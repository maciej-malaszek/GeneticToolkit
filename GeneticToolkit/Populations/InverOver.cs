using GeneticToolkit.Genotypes.Collective.Combinatory;
using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils.Extensions;
using GeneticToolkit.Utils.Factories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GeneticToolkit.Populations
{
    public class InverOverPopulation : PopulationBase
    {
        public float ChangeParentProbability { get; set; }
        
        public InverOverPopulation() {}
        
        public InverOverPopulation(IFitnessFunction fitnessFunction, float changeParentProbability, int size)
        {
            FitnessFunction = fitnessFunction;
            ChangeParentProbability = changeParentProbability;
            Individuals = new IIndividual[size];
        }

        public override void NextGeneration()
        {
            int genotypeLength = ((CombinatoryGenotype) Individuals[0].Genotype).Count;
            for (var individualIndex = 0; individualIndex < Size; individualIndex++)
            {
                IIndividual firstIndividual = Individuals[individualIndex];
                var firstParent = (CombinatoryGenotype) firstIndividual.Genotype;
                var child = new short[genotypeLength];
                for (var i = 0; i < genotypeLength; i++)
                    child[i] = firstParent.Value[i];

                int index0 = Random.Next(genotypeLength - 2);
                do
                {
                    int index1 = Random.NextDouble() <= ChangeParentProbability
                        ? GetEndIndexFromSameParent(index0, genotypeLength)
                        : GetEndIndexFromOtherParent(firstParent, index0);

                    index1 = --index1 >= 0 ? index1 : genotypeLength - 1;
                    FixIndexOrder(ref index0, ref index1);
                    if (SelectedNeighbours(index0, index1))
                        break;

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