using System;
using System.Collections;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Individuals
{
    public class Individual : IIndividual
    {
        private static readonly Random Rng = new Random();

        public IGenotype Genotype { get; set; }

        public IPhenotype Phenotype { get; }

        public void Mutate(IMutationPolicy policy)
        {
            BitArray mask = new BitArray(Genotype.Length);
            for(int i = 0; i < policy.MutatedGenesCount; i++)
                if(Rng.NextDouble() < policy.MutationChance)
                    mask[Rng.Next(Genotype.Length)] = true;
            Genotype.Genes = Genotype.Genes.Xor(mask);
        }

        public int CompareTo(IIndividual other, ICompareCriteria criteria)
        {
            return criteria.Compare(this, other);
        }

        public Individual(IGenotype genotype, IPhenotype phenotype)
        {
            Genotype = genotype;
            Phenotype = phenotype;
            Phenotype.Genotype = genotype;
        }
    }
}
