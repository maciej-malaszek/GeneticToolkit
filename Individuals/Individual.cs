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

        public IIndividual CrossOver(ICrossOverPolicy policy, params IIndividual[] parents)
        {
            Individual child = new Individual(Genotype.ShallowCopy(), Phenotype.ShallowCopy());
            child.Phenotype.Genotype = child.Genotype;
            int counter = 0;
            int startCut=0, endCut = 0;
            foreach(IIndividual parent in parents)
            {
                for(int j = 0; j < policy.CutPointsPerParent; j++)
                {
                    endCut = policy.GetCutPoint(Genotype.Length, counter++,startCut);
                    BitArray maskArray = new BitArray(endCut - startCut, true)
                    { Length = Genotype.Length };
                    maskArray.LeftShift(startCut);
                    child.Genotype.Genes = child.Genotype.Genes.Or(maskArray.And(parent.Genotype.Genes));
                    startCut = endCut;
                }
            }

            return child;
        }

        public Individual(IGenotype genotype, IPhenotype phenotype)
        {
            Genotype = genotype;
            Phenotype = phenotype;
            Phenotype.Genotype = genotype;
        }
    }
}
