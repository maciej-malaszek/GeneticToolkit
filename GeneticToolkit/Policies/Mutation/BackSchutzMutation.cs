using GeneticToolkit.Interfaces;

using System;

namespace GeneticToolkit.Policies.Mutation
{
    public class BackSchutzMutation : IMutationPolicy
    {
        public float GetMutationChance(IPopulation population)
        {
            return (float)(1.0/(2+CalculationsTotalTime*(population[0].Genotype.Length*8-2)/CalculationsTotalTime));
        }

        public float CalculationsTotalTime { get; set; }
        public float CalculationTime { get; set; }
        public float MutatedGenesPercent { get; }

        public Func<float> GetCalculationTime { get; set; }

        public BackSchutzMutation(float calculationsTotalTime, Func<float> getCalculationTime)
        {
            CalculationsTotalTime = calculationsTotalTime;
            GetCalculationTime = getCalculationTime;
        }
        
        public BackSchutzMutation(){}
    }
}
