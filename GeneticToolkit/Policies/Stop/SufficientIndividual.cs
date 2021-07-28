using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils.Exceptions;
using JetBrains.Annotations;

namespace GeneticToolkit.Policies.Stop
{
    [PublicAPI]
    public class SufficientIndividual<TFitnessFunctionFactory> : IStopCondition where TFitnessFunctionFactory : IFitnessFunctionFactory, new()
    {
        private static IFitnessFunction _fitnessFunction;
        public double SufficientResult { get; set; }

        public bool Satisfied(IEvolutionaryPopulation population)
        {
            if (population == null)
            {
                throw new PopulationNotInitializedException();
            }

            if (_fitnessFunction == null)
            {
                throw new FitnessFunctionNotInitializedException();
            }

            var comparisonResult = _fitnessFunction.GetValue(population.Best).CompareTo(SufficientResult);
            comparisonResult *= population.CompareCriteria.OptimizationMode == EOptimizationMode.Minimize ? -1 : 1;
            return comparisonResult >= 0;
        }
        public SufficientIndividual(double sufficientResult)
        {
            _fitnessFunction ??= new TFitnessFunctionFactory().Make();
            SufficientResult = sufficientResult;
        }

        public SufficientIndividual()
        {
            _fitnessFunction ??= new TFitnessFunctionFactory().Make();
        }
    }
}