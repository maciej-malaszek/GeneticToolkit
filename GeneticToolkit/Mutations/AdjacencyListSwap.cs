using GeneticToolkit.Genotypes.Collective.Combinatory;
using GeneticToolkit.Interfaces;
using System;
using JetBrains.Annotations;

namespace GeneticToolkit.Mutations
{
    [PublicAPI]
    public class AdjacencyListSwap : IMutation
    {
        private readonly Random _random = new();

        public void Mutate(IGenotype genotype, IMutationPolicy mutationPolicy, IPopulation population)
        {
            if (genotype is not AdjacencyListGenotype gen)
            {
                return;
            }

            var decoded = gen.GetDecoded();
            for (var i = 0; i < mutationPolicy.MutatedGenesPercent * gen.Count; i++)
            {
                if (_random.Next() < mutationPolicy.GetMutationChance(population))
                {
                    SwapGenes(ref decoded, _random.Next(0, gen.Count), _random.Next(0, gen.Count));
                }
            }

            gen.Encode(decoded);
        }

        private static void SwapGenes(ref short[] decoded, int index0, int index1)
        {
            var t = decoded[index0];
            decoded[index0] = decoded[index1];
            decoded[index1] = t;
        }
    }
}