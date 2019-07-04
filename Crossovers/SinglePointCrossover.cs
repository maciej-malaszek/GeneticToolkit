﻿using GeneticToolkit.Genotypes;
using GeneticToolkit.Interfaces;

using System;

namespace GeneticToolkit.Crossovers
{
    public class SinglePointCrossover : ICrossover
    {
        protected Random RandomNumberGenerator { get; set; } = new Random();
        public int ParentsCount { get; } = 2;
        public int ChildrenCount { get; } = 2;
        public int BitAlign { get; set; } = 1;

        public SinglePointCrossover() { }

        public IGenotype[] Cross(IGenotype[] parents)
        {
            int genotypeLength = parents[0].Length; // in bytes
            IGenotype[] children = { parents[0].ShallowCopy(), parents[1].ShallowCopy() };

            var cutIndex = BitAlign * RandomNumberGenerator.Next(genotypeLength * GenotypeBase.BitsPerGene / BitAlign);
            byte[] mask = new byte[genotypeLength];
            for (int i = 0; i < cutIndex / GenotypeBase.BitsPerGene; i++)
                mask[i] = byte.MaxValue;

            mask[cutIndex / GenotypeBase.BitsPerGene] = (byte) ((1u << cutIndex + 1)-1);

            for (int i = cutIndex / GenotypeBase.BitsPerGene + 1; i < genotypeLength; i++)
                mask[i] = byte.MinValue;

            for (int i = 0; i < genotypeLength; i++)
                children[0].Genes[i] = (byte) ((parents[0].Genes[i] & mask[i]) | (parents[1].Genes[i] & ~mask[i]));

            for (int i = 0; i < genotypeLength; i++)
                children[1].Genes[i] = (byte) ((parents[1].Genes[i] & mask[i]) | (parents[0].Genes[i] & ~mask[i]));
            return children;
        }

    }
}
