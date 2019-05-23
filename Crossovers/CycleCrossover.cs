using GeneticToolkit.Genotypes.Collective.Combinatory;
using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils.Data;

namespace GeneticToolkit.Crossovers
{
    /// <summary>
    /// Also known as CX
    /// </summary>
    public class CycleCrossover : ICrossover
    {
        public CycleCrossover()
        {
        }

        public CycleCrossover(ContemptibleDictionary<string, object> parameters) { }

        public int ParentsCount => 2;
        public int ChildrenCount => 2;
        public int BitAlign { get; set; }
        public IGenotype[] Cross(IGenotype[] parentsBase)
        {

            var parents = new PermutationGenotype[ParentsCount];
            var children = new PermutationGenotype[ChildrenCount];

            for (int j = 0; j < ParentsCount; j++)
                parents[j] = parentsBase[j] as PermutationGenotype;
            for (int j = 0; j < ChildrenCount; j++)
                children[j] = parents[0].EmptyCopy<PermutationGenotype>();

            var childrenValues = new short[ChildrenCount][];
            var availableIndexes = new bool[ChildrenCount][];
            var genotypeSize = parents[0].Count;

            for (int childIndex = 0; childIndex < ChildrenCount; childIndex++)
            {
                childrenValues[childIndex] = new short[genotypeSize];
                availableIndexes[childIndex] = new bool[genotypeSize];
                for (int i = 0; i < genotypeSize; i++)
                    availableIndexes[childIndex][i] = true;
                var nextParentIndex = childIndex + 1 >= ParentsCount ? 0 : childIndex + 1;

                int index = 0;
                while (availableIndexes[childIndex][index])
                {
                    childrenValues[childIndex][index] = parents[childIndex].Value[index];
                    availableIndexes[childIndex][index] = false;
                    index = parents[childIndex].GetIndex(parents[nextParentIndex].Value[index]);
                }

                for (int i = 0; i < genotypeSize; i++)
                    if (availableIndexes[childIndex][i])
                        childrenValues[childIndex][i] = parents[nextParentIndex].Value[i];
            }

            for (int j = 0; j < ChildrenCount; j++)
                children[j].Value = childrenValues[j];

            return children;
        }
    }
}
