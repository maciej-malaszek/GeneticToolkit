using System.Collections.Generic;
using GeneticToolkit.Genotypes.Collective.Combinatory;
using GeneticToolkit.Interfaces;
using JetBrains.Annotations;

namespace GeneticToolkit.Crossovers
{
    /// <summary>
    /// Also known as CX
    /// </summary>
    [PublicAPI]
    public class CycleCrossover : ICrossover
    {
        public CycleCrossover() {  }

        public CycleCrossover(Dictionary<string, object> parameters) {  }

        public int ParentsCount => 2;
        public int ChildrenCount => 2;
        public int BitAlign { get; set; }

        public IGenotype[] Cross(IGenotype[] parents)
        {
            var parentsPermutation = new PermutationGenotype[ParentsCount];
            var children = new IGenotype[ChildrenCount];

            for (var j = 0; j < ParentsCount; j++)
            {
                parentsPermutation[j] = parents[j] as PermutationGenotype;
            }

            for (var j = 0; j < ChildrenCount; j++)
            {
                children[j] = parentsPermutation[0].EmptyCopy<PermutationGenotype>();
            }

            var childrenValues = new short[ChildrenCount][];
            var availableIndexes = new bool[ChildrenCount][];
            var genotypeSize = parentsPermutation[0].Count;

            for (var childIndex = 0; childIndex < ChildrenCount; childIndex++)
            {
                childrenValues[childIndex] = new short[genotypeSize];
                availableIndexes[childIndex] = new bool[genotypeSize];
                for (var i = 0; i < genotypeSize; i++)
                {
                    availableIndexes[childIndex][i] = true;
                }

                var nextParentIndex = childIndex + 1 >= ParentsCount ? 0 : childIndex + 1;

                var index = 0;
                while (availableIndexes[childIndex][index])
                {
                    childrenValues[childIndex][index] = parentsPermutation[childIndex].Value[index];
                    availableIndexes[childIndex][index] = false;
                    index = parentsPermutation[childIndex].GetIndex(parentsPermutation[nextParentIndex].Value[index]);
                }

                for (var i = 0; i < genotypeSize; i++)
                {
                    if (availableIndexes[childIndex][i])
                    {
                        childrenValues[childIndex][i] = parentsPermutation[nextParentIndex].Value[i];
                    }
                }
            }

            for (var j = 0; j < ChildrenCount; j++)
            {
                ((PermutationGenotype) children[j]).Value = childrenValues[j];
            }

            return children;
        }
    }
}