using GeneticToolkit.Genotypes.Collective.Combinatory;
using GeneticToolkit.Interfaces;
using System;
using GeneticToolkit.Utils.Extensions;
using JetBrains.Annotations;

namespace GeneticToolkit.Crossovers
{
    [PublicAPI]
    public class SubTourChunkCrossover : ICrossover
    {
        private readonly Random _random = new();

        public int ParentsCount { get; } = 2;
        public int ChildrenCount { get; } = 2;
        public int BitAlign { get; set; } = 1;


        protected int AvailableIndexCount;
        protected bool[] UsedIndexes;
        protected short[] AvailableIndexes;
        protected short[] AvailableIndexesReverse;
        protected short[] ChildValues;

        protected int SubTourIndex;
        protected int ParentIndex;
        protected int StartIndex;
        protected short Target;

        protected void InitializeVariables(AdjacencyListGenotype[] parents, int genotypeSize, int startingParent)
        {
            ChildValues = new short[genotypeSize];
            // Used to find edges that do not cause cycle
            UsedIndexes = new bool[genotypeSize];
            AvailableIndexCount = genotypeSize - 1;
            AvailableIndexes = new short[genotypeSize];
            AvailableIndexesReverse = new short[genotypeSize];
            for (var i = 0; i < genotypeSize; i++)
            {
                AvailableIndexes[i] = (short) i;
                AvailableIndexesReverse[i] = (short) i;
            }

            // Used to select parent
            SubTourIndex = startingParent;
            ParentIndex = ++SubTourIndex % ParentsCount;

            StartIndex = 0;
            Target = parents[ParentIndex].Value[StartIndex];

            // First index is always locked as it is the first one
            UsedIndexes[0] = true;
            AvailableIndexes.Swap(0, AvailableIndexCount);
            AvailableIndexesReverse.Swap(0, AvailableIndexCount);
        }

        protected virtual short[] GetChildValues(AdjacencyListGenotype[] parents, int genotypeSize, int startingParent)
        {

            InitializeVariables(parents, genotypeSize, startingParent);
            // 1. Choose length of subtour chunk. First subtour must not be too big.
            var subTourLength = _random.Next(AvailableIndexCount / 2);

            do
            {
                // 2. Select current parent
                ParentIndex = ++SubTourIndex % ParentsCount;

                for (var i = 0; i < subTourLength; i++)
                {
                    // 3. Insert target - already assured that is OK
                    ChildValues[StartIndex] = GetTarget(parents);
                }

                // 10. Select next chunk length
                subTourLength = _random.Next(AvailableIndexCount + 1);
            } while (AvailableIndexCount > 0);

            return ChildValues;
        }
        

        protected short GetTarget(AdjacencyListGenotype[] parents)
        {
            // 4. Block inserted target
            UsedIndexes[Target] = true;
            // 5. Update list of available indexes. Blocked indexes are always at the end of array.
            AvailableIndexCount--;

            // 5A. Swap locked and available items
            AvailableIndexes.Swap(AvailableIndexesReverse[Target], AvailableIndexCount);
            // 5B. Swap indexes of locked and available items. Available item is already swapped with target, so use target as index
            AvailableIndexesReverse.Swap(Target, AvailableIndexes[AvailableIndexesReverse[Target]]);

            // 6. Set last target as new start
            StartIndex = Target;

            // 7. Select candidate for new target
            Target = parents[ParentIndex].Value[StartIndex];

            // 8. If candidate was used and there are left unused indexes
            if (UsedIndexes[Target] && AvailableIndexCount > 0)
                // 9. Select random target from list of available                            
            {
                Target = AvailableIndexes[_random.Next(0, AvailableIndexCount)];
            }

            return Target;
        }

        public IGenotype[] Cross(IGenotype[] parents)
        {
            var parentGenotypes = new AdjacencyListGenotype[parents.Length];
            for (var i = 0; i < parents.Length; i++)
            {
                parentGenotypes[i] = parents[i] as AdjacencyListGenotype;
            }

            var children = new IGenotype[ChildrenCount];
            var genotypeSize = parentGenotypes[0].Count;

            for (var j = 0; j < ChildrenCount; j++)
            {
                var child = parentGenotypes[0].EmptyCopy<AdjacencyListGenotype>();
                child.Value = GetChildValues(parentGenotypes, genotypeSize, j);
                children[j] = child;
            }

            return children;
        }
    }
}