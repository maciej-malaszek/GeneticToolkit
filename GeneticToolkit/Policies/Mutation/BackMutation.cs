using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Policies.Mutation
{
    public class BackMutation : IMutationPolicy
    {
        public float GetMutationChance(IPopulation population)
        {
            return (float)(1.0 / (2 * (population.FitnessFunction.GetValue(population.Best) + 1) - population.Best.Genotype.Length * 8));
        }
        public float MutationChanceMultiplier { get; set; }
        public float MutationChanceOffset { get; set; }
        public float MutatedGenesPercent { get; }
        public BackMutation(float mutatedGenesPercent, float mutationChanceMultiplier, float mutationChanceOffset)
        {
            MutatedGenesPercent = mutatedGenesPercent;
            MutationChanceMultiplier = mutationChanceMultiplier;
            MutationChanceOffset = mutationChanceOffset;
        }
    }
}
