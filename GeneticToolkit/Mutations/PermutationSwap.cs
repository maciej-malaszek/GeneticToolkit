using GeneticToolkit.Genotypes.Collective.Combinatory;
using GeneticToolkit.Interfaces;
using System;
using JetBrains.Annotations;

namespace GeneticToolkit.Mutations
{
    [PublicAPI]
    public class PermutationSwap : IMutation
    {
        private readonly Random _random = new Random();


        public void Mutate(IGenotype genotype, IMutationPolicy mutationPolicy, IPopulation population)
        {
            if (!(genotype is CombinatoryGenotype gen))
                return;

            for (var i = 0; i < mutationPolicy.MutatedGenesPercent * gen.Count; i++)
                if (_random.Next() < mutationPolicy.GetMutationChance(population))
                    SwapGenes(gen, _random.Next(0, gen.Count), _random.Next(0, gen.Count));
        }

        private static void SwapGenes(CombinatoryGenotype genotype, int index0, int index1)
        {
            short t = genotype.Value[index0];
            genotype.SetValue(index0, genotype.Value[index1]);
            genotype.SetValue(index1, t);
        }
    }
}