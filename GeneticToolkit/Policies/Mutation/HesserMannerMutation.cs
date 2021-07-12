using GeneticToolkit.Interfaces;

using System;

namespace GeneticToolkit.Policies.Mutation
{
    public class HesserMannerMutation : IMutationPolicy
    {
        private float _alpha;
        public float Alpha { get => _alpha; set => _alpha = Math.Abs(value); }
        public float Beta { get; set; }
        public float GetMutationChance(IPopulation population)
        {
            return (float)(Alpha * Math.Pow(Math.E, -Beta * population.Generation / 2.0) / (population.Size * Math.Sqrt(population[0].Genotype.Length*8)));
        }
        public float MutatedGenesPercent { get; }

        public HesserMannerMutation(float alpha, float beta, float mutatedGenesPercent)
        {
            MutatedGenesPercent = mutatedGenesPercent;
            Alpha = Math.Abs(alpha);
            Beta = beta;
        }
        public HesserMannerMutation() {}
    }
}
