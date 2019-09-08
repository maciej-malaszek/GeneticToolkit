using GeneticToolkit.Interfaces;

using System;

namespace GeneticToolkit.Crossovers
{
    public class UniformCrossover : ICrossover
    {
        protected Random RandomNumberGenerator { get; set; } = new Random();
        public int ParentsCount { get; set; }
        public int ChildrenCount { get; set; }
        public int BitAlign { get; set; } = 8;
        public IGenotype[] Cross(IGenotype[] parents)
        {
            int genotypeLength = parents[0].Length; // in bytes
            var children = new IGenotype[ChildrenCount];

            for (int i = 0; i < ChildrenCount; i++)
                children[i] = parents[0].ShallowCopy();

            for (int child = 0; child < ChildrenCount; child++)
                for (int i = 0; i < genotypeLength * 8; i += BitAlign)
                {
                    int parentId = RandomNumberGenerator.Next(parents.Length);
                    if (BitAlign == 8)
                        children[child][i/8] = parents[parentId][i/8];
                    else
                        children[child].SetBit(i, parents[parentId].GetBit(i));
                }

            return children;
        }
    }
}
