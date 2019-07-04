using GeneticToolkit.Genotypes.Collective.Combinatory;
using GeneticToolkit.Interfaces;

using System;

namespace GeneticToolkit.Crossovers
{

    /// <summary>
    /// Also known as PMX
    /// </summary>
    public class PartiallyMappedCrossover : ICrossover
    {
        protected Random RandomNumberGenerator { get; set; } = new Random();

        public PartiallyMappedCrossover()
        {

        }

        public int ParentsCount => 2;
        public int ChildrenCount => 2;
        /// <summary>
        /// Not used in this implementation
        /// </summary>
        public int BitAlign { get; set; }
        public IGenotype[] Cross(IGenotype[] parents)
        {
            PermutationGenotype[] parent = { parents[0] as PermutationGenotype, parents[1] as PermutationGenotype };
            PermutationGenotype[] children = { parents[0].EmptyCopy<PermutationGenotype>(), parents[1].EmptyCopy<PermutationGenotype>() };

            int genotypeSize = parent[0].Count;
            var childrenValues = new short[ChildrenCount][];

            for (int i = 0; i < ChildrenCount; i++)
                childrenValues[i] = new short[children[0].Count];

            var startCutIndex = RandomNumberGenerator.Next(genotypeSize - 3);
            var endCutIndex = RandomNumberGenerator.Next(startCutIndex+1, genotypeSize);

            for (int i = 0; i < genotypeSize; i++)
            {                
                for (int j = 0; j < ChildrenCount; j++)
                {
                    int currentParentId = j;
                    int nextParentId = j + 1 >= ParentsCount ? 0 : j + 1;

                    if (i >= startCutIndex && i <= endCutIndex)
                        childrenValues[currentParentId][i] = parent[nextParentId].Value[i];
                    else
                    {
                        childrenValues[currentParentId][i] = GetValue(parent[currentParentId], parent[nextParentId], parent[currentParentId].Value[i],
                            startCutIndex, endCutIndex);
                    }
                    
                }
            }
            for (int j = 0; j < ChildrenCount; j++)
                children[j].Value = childrenValues[j];

            return children;
        }

        public short GetValue(PermutationGenotype parent, PermutationGenotype otherParent,  short value, int startIndex, int endIndex)
        {
            int index = otherParent.GetIndex(value);

            while (index >= startIndex && index <= endIndex)
            {
                value = parent.Value[index];
                index = otherParent.GetIndex(value);
            }
            return value;
        }
    }
}
