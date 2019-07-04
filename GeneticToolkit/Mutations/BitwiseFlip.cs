using GeneticToolkit.Genotypes;
using GeneticToolkit.Interfaces;
using System;
using JetBrains.Annotations;

namespace GeneticToolkit.Mutations
{
    [PublicAPI]
    public class BitwiseFlip : IMutation
    {
        private readonly Random _random = new Random();

        public void Mutate(IGenotype genotype, IMutationPolicy mutationPolicy, IPopulation population)
        {
            for (var i = 0; i < mutationPolicy.MutatedGenesPercent * genotype.Length; i++)
                if (_random.NextDouble() < mutationPolicy.GetMutationChance(population))
                {
                    int index = _random.Next(genotype.Length * GenotypeBase.BitsPerGene);
                    genotype.SetBit(index, !genotype.GetBit(index));
                }
        }
    }
}