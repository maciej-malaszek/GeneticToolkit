using GeneticToolkit.Genotypes;
using GeneticToolkit.Interfaces;

using System;

namespace GeneticToolkit.Mutations
{
    public class BitwiseFlip : IMutation
    {
        private readonly Random _random = new Random();
        public void Mutate(IGenotype genotype, IMutationPolicy mutationPolicy, IPopulation population)
        {
            for (int i = 0; i < mutationPolicy.MutatedGenesPercent * genotype.Length; i++)
                if (_random.NextDouble() < mutationPolicy.GetMutationChance(population))
                {
                    int index = _random.Next(genotype.Length * GenotypeBase.BitsPerGene);
                    genotype.SetBit(index, !genotype.GetBit(index));
                }
        }

        public BitwiseFlip() { }

    }
}
