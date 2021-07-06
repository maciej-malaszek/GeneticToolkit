using GeneticToolkit.Interfaces;
using JetBrains.Annotations;

namespace GeneticToolkit.Comparisons
{
    [PublicAPI]
    public class SimpleComparison : ICompareCriteria
    {
        public IFitnessFunction FitnessFunction { get; set; }
        
        public SimpleComparison() {}

        public SimpleComparison(IFitnessFunction fitnessFunction, EOptimizationMode optimizationMode = EOptimizationMode.Maximize)
        {
            OptimizationMode = optimizationMode;
            FitnessFunction = fitnessFunction;
        }

        public EOptimizationMode OptimizationMode { get; private set; }

        public IIndividual GetBetter( IIndividual  x1,  IIndividual  x2)
        {
            if (x1 == null)
                return x2;
            if (x2 == null)
                return x1;
            return Compare(x1, x2) <= 0 ? x1 : x2;
        }

        public int Compare( IIndividual  x1,  IIndividual  x2)
        {
            int result = FitnessFunction.GetValue(x1).CompareTo(FitnessFunction.GetValue(x2));
            if (OptimizationMode == EOptimizationMode.Maximize)
                result *= -1;
            return result;
        }
    }
}
