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
        private readonly Random _random = new Random();

        public int ParentsCount { get; } = 2;
        public int ChildrenCount { get; } = 2;
        public int BitAlign { get; set; } = 1;

        private short[] GetChildValues(AdjacencyListGenotype[] parents, int genotypeSize, int startingParent)
        {
            var childValues = new short[genotypeSize];

            // Used to find edges that do not cause cycle
            var usedIndexes = new bool[genotypeSize];
            int availableIndexCount = genotypeSize - 1;

            var availableIndexes = new short[genotypeSize];
            var availableIndexesReverse = new short[genotypeSize];
            for (var i = 0; i < genotypeSize; i++)
            {
                availableIndexes[i] = (short) i;
                availableIndexesReverse[i] = (short) i;
            }

            // Used to select parent
            int subTourIndex = startingParent;
            int parentIndex = ++subTourIndex % ParentsCount;

            var startIndex = 0;
            short target = parents[parentIndex].Value[startIndex];


            // First index is always locked as it is the first one
            usedIndexes[0] = true;
            availableIndexes.Swap(0, availableIndexCount);
            availableIndexesReverse.Swap(0, availableIndexCount);

            // 1. Choose length of subtour chunk. First subtour must not be too big.
            int subTourLength = _random.Next(availableIndexCount / 2);

            do
            {
                // 2. Select current parent
                parentIndex = ++subTourIndex % ParentsCount;

                for (var i = 0; i < subTourLength; i++)
                {
                    // 3. Insert target - already assured that is OK
                    childValues[startIndex] = target;

                    // 4. Block inserted target
                    usedIndexes[target] = true;
                    // 5. Update list of available indexes. Blocked indexes are always at the end of array.
                    availableIndexCount--;

                    // 5A. Swap locked and available items
                    availableIndexes.Swap(availableIndexesReverse[target], availableIndexCount);
                    // 5B. Swap idexes of locked and available items. Available item is already swapped with target, so use target as index
                    availableIndexesReverse.Swap(target, availableIndexes[availableIndexesReverse[target]]);

                    // 6. Set last target as new start
                    startIndex = target;

                    // 7. Select candidate for new target
                    target = parents[parentIndex].Value[startIndex];

                    // 8. If candidate was used and there are left unused indexes
                    if (usedIndexes[target] && availableIndexCount > 0)
                        // 9. Select random target from list of available                            
                        target = availableIndexes[_random.Next(0, availableIndexCount)];
                }

                // 10. Select next chunk length
                subTourLength = _random.Next(availableIndexCount + 1);
            } while (availableIndexCount > 0);

            return childValues;
        }

        public IGenotype[] Cross(IGenotype[] baseParents)
        {
            var parents = new AdjacencyListGenotype[baseParents.Length];
            for (var i = 0; i < baseParents.Length; i++)
                parents[i] = baseParents[i] as AdjacencyListGenotype;

            var children = new IGenotype[ChildrenCount];
            int genotypeSize = parents[0].Count;

            for (var j = 0; j < ChildrenCount; j++)
            {
                var child = parents[0].EmptyCopy<AdjacencyListGenotype>();
                child.Value = GetChildValues(parents, genotypeSize, j);
                children[j] = child;
            }

            return children;
        }
    }
}