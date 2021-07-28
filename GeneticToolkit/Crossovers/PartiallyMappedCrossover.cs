using GeneticToolkit.Genotypes.Collective.Combinatory;
using GeneticToolkit.Interfaces;
using System;
using JetBrains.Annotations;

namespace GeneticToolkit.Crossovers
{
    /// <summary>
    /// Also known as PMX
    /// </summary>
    [PublicAPI]
    public class PartiallyMappedCrossover : ICrossover
    {
        protected Random RandomNumberGenerator { get; set; } = new();

        public int ParentsCount => 2;
        public int ChildrenCount => 2;

        /// <summary>
        /// Not used in this implementation
        /// </summary>
        public int BitAlign { get; set; }

        private int _startCutIndex;
        private int _endCutIndex;
        private int _genotypeSize;

        private void UpdateCutIndexes()
        {
            _startCutIndex = RandomNumberGenerator.Next(_genotypeSize - 3);
            _endCutIndex = RandomNumberGenerator.Next(_startCutIndex + 1, _genotypeSize);
        }

        private short[] GetValuesForChild(PermutationGenotype[] parent, int currentParentId, int nextParentId)
        {
            var childValues = new short[_genotypeSize];
                
            for (var i = 0; i < _genotypeSize; i++)
            {
                if (i >= _startCutIndex && i <= _endCutIndex)
                {
                    childValues[i] = parent[nextParentId].Value[i];
                }
                else
                {
                    childValues[i] = GetValue(
                        parent[currentParentId], parent[nextParentId],
                        parent[currentParentId].Value[i]);
                }
            }

            return childValues;

        }

        public IGenotype[] Cross(IGenotype[] parents)
        {
            PermutationGenotype[] parent = {parents[0] as PermutationGenotype, parents[1] as PermutationGenotype};
            IGenotype[] children =
                {parents[0].EmptyCopy<PermutationGenotype>(), parents[1].EmptyCopy<PermutationGenotype>()};

            _genotypeSize = parent[0].Count;
            UpdateCutIndexes();

            for (var currentParentId = 0; currentParentId < ChildrenCount; currentParentId++)
            {
                var nextParentId = currentParentId + 1 >= ParentsCount ? 0 : currentParentId + 1;
                ((PermutationGenotype) children[currentParentId]).Value = GetValuesForChild(parent, currentParentId, nextParentId);
            }

            return children;
        }

        public short GetValue(PermutationGenotype parent, PermutationGenotype otherParent, short value)
        {
            var index = otherParent.GetIndex(value);

            while (index >= _startCutIndex && index <= _endCutIndex)
            {
                value = parent.Value[index];
                index = otherParent.GetIndex(value);
            }

            return value;
        }
    }
}