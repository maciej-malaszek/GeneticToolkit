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

        private int _startCutIndex;
        private int _endCutIndex;
        private int _genotypeSize;

        private short[] GetSecondaryPointForChild(PermutationGenotype[] parents, int currentParentId, int nextParentId)
        {
            var otherPointsIndex = 0;
            var secondaryPoints = new short[_genotypeSize - (_endCutIndex - _startCutIndex + 1)];
            for (var i = 0; i < _genotypeSize; i++)
            {
                int x = parents[currentParentId].GetIndex(parents[nextParentId].Value[i]);
                if (x < _startCutIndex || x > _endCutIndex)
                    secondaryPoints[otherPointsIndex++] = parents[nextParentId].Value[i];
            }

            return secondaryPoints;
        }

        private short[][] GetSecondaryPoints(PermutationGenotype[] parents)
        {
            var secondaryPoints = new short[ChildrenCount][];

            for (var j = 0; j < ChildrenCount; j++)
            {
                int currentParentId = j;
                int nextParentId = j + 1 >= ParentsCount ? 0 : j + 1;
                secondaryPoints[currentParentId] = GetSecondaryPointForChild(parents, currentParentId, nextParentId);
            }

            return secondaryPoints;
        }

        private void InsertPointsIntoChild(short[] childSecondaryPoints, short[] childValues)
        {
            var index = 0;
            int childIndex = _endCutIndex;
            while (index < childSecondaryPoints.Length)
            {
                childIndex++;
                if (childIndex >= _genotypeSize)
                    childIndex = 0;
                childValues[childIndex] = childSecondaryPoints[index++];
            }
        }

        private short[] GetValuesForChild(PermutationGenotype parent, short[] secondaryPoints)
        {
            var childValues = new short[_genotypeSize];
            for (int i = _startCutIndex; i <= _endCutIndex; i++)
                childValues[i] = parent.Value[i];
            InsertPointsIntoChild(secondaryPoints, childValues);
            return childValues;
        }

        public IGenotype[] Cross(IGenotype[] parents)
        {
            PermutationGenotype[] parent = {parents[0] as PermutationGenotype, parents[1] as PermutationGenotype};
            IGenotype[] children =
                {parents[0].EmptyCopy<PermutationGenotype>(), parents[1].EmptyCopy<PermutationGenotype>()};

            _genotypeSize = parent[0].Count;
            _startCutIndex = RandomNumberGenerator.Next(_genotypeSize - 2);
            _endCutIndex = RandomNumberGenerator.Next(_startCutIndex + 1, _genotypeSize);
            
            var childrenValues = new short[ChildrenCount][];
            short[][] secondaryPoints = GetSecondaryPoints(parent);

            for (var j = 0; j < ChildrenCount; j++)
            {
                childrenValues[j] = GetValuesForChild(parent[j], secondaryPoints[j]);
                ((PermutationGenotype) children[j]).Value = childrenValues[j];
            }

            return children;
        }
    }
}