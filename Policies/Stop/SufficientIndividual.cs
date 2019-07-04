using System;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Policies.Stop
{
    public class SufficientIndividual : IStopCondition
    {
        public IFitnessFunction FitnessFunction { get; set; }

        public double SufficientResult { get; set; }

        public bool Satisfied(IEvolutionaryPopulation population)
        {
            if(population == null)
                throw new NullReferenceException("Population has not been initialized!");
            if(FitnessFunction == null)
                throw new NullReferenceException("Fitness function has not been initialized!");

            var comparisonResult = FitnessFunction.GetValue(population.Best).CompareTo(SufficientResult);
            comparisonResult *= population.CompareCriteria.OptimizationMode == EOptimizationModeOrder.Minimize ? -1 : 1;
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
