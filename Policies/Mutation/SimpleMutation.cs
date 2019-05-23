using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Policies.Mutation
{
    public class SimpleMutation : IMutationPolicy
    {
        public float MutationChance { get; protected set; }
        public float GetMutationChance(IPopulation population) => MutationChance;
        public float MutatedGenesPercent { get; protected set; }

        public SimpleMutation(float mutationChance = 0.1f, float mutatedGenesPercent = 0.5f)
        {
            MutationChance = mutationChance;
            MutatedGenesPercent = mutatedGenesPercent;
        }
    }
}
