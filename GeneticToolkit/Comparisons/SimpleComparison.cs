using GeneticToolkit.Interfaces;
using JetBrains.Annotations;

namespace GeneticToolkit.Comparisons
{
    [PublicAPI]
    public class SimpleComparison<TFitnessFunctionFactory> : ICompareCriteria where TFitnessFunctionFactory : IFitnessFunctionFactory, new()
    {
        private static IFitnessFunction _fitnessFunction;
        
        public SimpleComparison() {}

        public SimpleComparison(EOptimizationMode optimizationMode = EOptimizationMode.Maximize)
        {
            OptimizationMode = optimizationMode;
            _fitnessFunction ??= new TFitnessFunctionFactory().Make();
        }

        public EOptimizationMode OptimizationMode { get; private set; }

        public IIndividual GetBetter( IIndividual  x1,  IIndividual  x2)
        {
            if (x1 == null)
            {
                return x2;
            }

            return x2 == null ? x1 : Compare(x1, x2) <= 0 ? x1 : x2;
        }

        public int Compare( IIndividual  x1,  IIndividual  x2)
        {
            var result = _fitnessFunction.GetValue(x1).CompareTo(_fitnessFunction.GetValue(x2));
            if (OptimizationMode == EOptimizationMode.Maximize)
            {
                result *= -1;
            }

            return result;
        }
    }
}
