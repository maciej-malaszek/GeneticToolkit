﻿using GeneticToolkit.Genotypes;
using GeneticToolkit.Interfaces;

using System;

namespace GeneticToolkit.Crossovers
{
    public class MultiPointCrossover : ICrossover
    {
        public int ParentsCount { get; set; }
        public int ChildrenCount { get; set; }
        public int BitAlign { get; set; } = 1;
        public int CutPointCount { get; set; }
        protected Random RandomNumberGenerator { get; set; } = new Random();

        public MultiPointCrossover(int parentsCount, int childrenCount, int cutPointCount)
        {
            ParentsCount = parentsCount;
            ChildrenCount = childrenCount;
            CutPointCount = cutPointCount;
        }

        public IGenotype[] Cross(IGenotype[] parents)
        {
            return Cross(parents, ChildrenCount);
        }
        public IGenotype[] Cross(IGenotype[] parents, int childrenCount)
        {
            int genotypeLength = parents[0].Length;
            int parentIndex = 0;
            IGenotype[] children = new IGenotype[childrenCount];
            for (int c = 0; c < childrenCount; c++)
            {
                IGenotype child = parents[0].EmptyCopy();
                int[] cutIndexes = GetCutPoints(genotypeLength);
                for (int i = 0; i < CutPointCount - 1; i++)
                {
                    byte[] mask = GetMask(genotypeLength, cutIndexes, i);
                    for (int j = 0; j < genotypeLength; j++)
                        child.Genes[j] |= (byte)(mask[j] & parents[parentIndex++].Genes[j]);

                    if (parentIndex >= parents.Length)
                        parentIndex = 0;
                }
                children[c] = child;
            }
            return children;
        }
        private int[] GetCutPoints(int genotypeLength)
        {
            int[] cutIndexes = new int[CutPointCount];
            for (int i = 0; i < CutPointCount; i++)
                cutIndexes[i] = RandomNumberGenerator.Next(genotypeLength - CutPointCount + i) * GenotypeBase.BitsPerGene;
            Array.Sort(cutIndexes);
            return cutIndexes;
        }
        private static byte[] GetMask(int genotypeLength, int[] cutIndexes, int index)
        {
            int startCut = 0, endCut = cutIndexes[index];
            byte[] mask = new byte[genotypeLength];

            if (index != 0)
                startCut = cutIndexes[index - 1];

            if (startCut / GenotypeBase.BitsPerGene == endCut / GenotypeBase.BitsPerGene)
            {
                var byteIndex = startCut / GenotypeBase.BitsPerGene;
                mask[byteIndex] = (byte)((1 << endCut % GenotypeBase.BitsPerGene + 1) - 1 - ((1 << startCut % GenotypeBase.BitsPerGene + 1) - 1));
                return mask;
            }
                

            for (int i = startCut / GenotypeBase.BitsPerGene + 1; i < (int)Math.Ceiling((float)endCut / GenotypeBase.BitsPerGene); i++)
                mask[i] = byte.MaxValue;

            int startIndex = startCut % GenotypeBase.BitsPerGene;
            int endIndex = endCut % GenotypeBase.BitsPerGene;


            mask[startCut / GenotypeBase.BitsPerGene] = (byte)((1 << startIndex + 1) - 1);
            mask[endCut / GenotypeBase.BitsPerGene] = (byte)((1 << endIndex + 1) - 1);
            return mask;
        }
      
    }
}
