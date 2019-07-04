using GeneticToolkit.Genotypes.Collective.Combinatory;
using GeneticToolkit.Interfaces;
using System;
using JetBrains.Annotations;

namespace GeneticToolkit.Crossovers
{
    /// <summary>
    /// Also known as OX
    /// </summary>
    [PublicAPI]
    public class OrderCrossover : ICrossover
    {
        protected Random RandomNumberGenerator { get; set; } = new Random();

        public int ParentsCount => 2;
        public int ChildrenCount => 2;

        /// <inheritdoc />
        /// <summary>
        /// Not used in this implementation
        /// </summary>
        public int BitAlign { get; set; }

        public IGenotype[] Cross(IGenotype[] parents)
        {
            PermutationGenotype[] parent = {parents[0] as PermutationGenotype, parents[1] as PermutationGenotype};
            IGenotype[] children =
                {parents[0].EmptyCopy<PermutationGenotype>(), parents[1].EmptyCopy<PermutationGenotype>()};

            int genotypeSize = parent[0].Count;
            var childrenValues = new short[ChildrenCount][];


            int startCutIndex = RandomNumberGenerator.Next(genotypeSize - 2);
            int endCutIndex = RandomNumberGenerator.Next(startCutIndex + 1, genotypeSize);
            var otherPoints = new short[ChildrenCount][];


            for (var j = 0; j < ChildrenCount; j++)
            {
                childrenValues[j] = new short[((PermutationGenotype)children[0]).Count];
                int currentParentId = j;
                int nextParentId = j + 1 >= ParentsCount ? 0 : j + 1;
                otherPoints[currentParentId] = new short[genotypeSize - (endCutIndex - startCutIndex + 1)];
                var otherPointsIndex = 0;
                for (var i = 0; i < genotypeSize; i++)
                {
                    int x = parent[currentParentId].GetIndex(parent[nextParentId].Value[i]);
                    if (x < startCutIndex || x > endCutIndex)
                        otherPoints[currentParentId][otherPointsIndex++] = parent[nextParentId].Value[i];
                }
            }

            for (var j = 0; j < ChildrenCount; j++)
            {
                for (int i = startCutIndex; i <= endCutIndex; i++)
                    childrenValues[j][i] = parent[j].Value[i];

                var index = 0;
                int childIndex = endCutIndex;
                while (index < otherPoints[j].Length)
                {
                    childIndex++;
                    if (childIndex >= genotypeSize)
                        childIndex = 0;
                    childrenValues[j][childIndex] = otherPoints[j][index++];
                }
            }

            for (var j = 0; j < ChildrenCount; j++)
                ((PermutationGenotype)children[j]).Value = childrenValues[j];

            return children;
        }
    }
}