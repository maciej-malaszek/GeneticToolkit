﻿using GeneticToolkit.Genotypes.Collective.Combinatory;
using GeneticToolkit.Interfaces;
using System;
using JetBrains.Annotations;

namespace GeneticToolkit.Mutations
{
    [PublicAPI]
    public class PermutationInversion : IMutation
    {
        private readonly Random _random = new();

        public float MaxPercentOfGenotype { get; set; }

        public void Mutate(IGenotype genotype, IMutationPolicy mutationPolicy, IPopulation population)
        {
            if (genotype is not CombinatoryGenotype gen)
            {
                return;
            }

            if (_random.NextDouble() > mutationPolicy.GetMutationChance(population))
            {
                return;
            }

            var start = _random.Next(gen.Count - 1);
            var end = _random.Next(start, Math.Min(gen.Count, (int) (MaxPercentOfGenotype * gen.Count) + start));
            var length = end - start;

            for (var i = 0; i < length / 2; i++)
            {
                SwapGenes(gen, start++, end--);
            }
        }

        private static void SwapGenes(CombinatoryGenotype genotype, int index0, int index1)
        {
            var t = genotype.Value[index0];
            genotype.SetValue(index0, genotype.Value[index1]);
            genotype.SetValue(index1, t);
        }
    }
}