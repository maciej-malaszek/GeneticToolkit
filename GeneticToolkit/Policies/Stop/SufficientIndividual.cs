using System;
using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils.Exceptions;
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
            {
                throw new PopulationNotInitializedException();
            }

            if (FitnessFunction == null)
            {
                throw new FitnessFunctionNotInitializedException();
            }

            var comparisonResult = FitnessFunction.GetValue(population.Best).CompareTo(SufficientResult);
            comparisonResult *= population.CompareCriteria.OptimizationMode == EOptimizationMode.Minimize ? -1 : 1;
            return comparisonResult >= 0;
        }
        public SufficientIndividual(IFitnessFunction fitnessFunction, double sufficientResult)
        {
            FitnessFunction = fitnessFunction;
            SufficientResult = sufficientResult;
        }
    }
}