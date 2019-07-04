using System;
using GeneticToolkit.Interfaces;
using JetBrains.Annotations;

namespace GeneticToolkit.Policies.Stop
{
    [PublicAPI]
    public class SufficientIndividual : IStopCondition
    {
        public IFitnessFunction FitnessFunction { get; set; }

        public double SufficientResult { get; set; }

        public bool Satisfied(IEvolutionaryPopulation population)
        {
            if (population == null)
                throw new NullReferenceException("Population has not been initialized!");
            if (FitnessFunction == null)
                throw new NullReferenceException("Fitness function has not been initialized!");

            int comparisonResult = FitnessFunction.GetValue(population.Best).CompareTo(SufficientResult);
            comparisonResult *= population.CompareCriteria.OptimizationMode == EOptimizationMode.Minimize ? -1 : 1;
            return comparisonResult >= 0;
        }

        public void Reset()
        {
        }

        public SufficientIndividual(IFitnessFunction fitnessFunction, double sufficientResult)
        {
            FitnessFunction = fitnessFunction;
            SufficientResult = sufficientResult;
        }
    }
}