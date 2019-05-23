using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Comparisons
{
    public class SimpleComparison : ICompareCriteria
    {
        public IFitnessFunction FitnessFunction { get; set; }

        public SimpleComparison(IFitnessFunction fitnessFunction, EOptimizationModeOrder optimizationMode = EOptimizationModeOrder.Maximize)
        {
            OptimizationMode = optimizationMode;
            FitnessFunction = fitnessFunction;
        }

        public EOptimizationModeOrder OptimizationMode { get; private set; }

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
            if (OptimizationMode == EOptimizationModeOrder.Maximize)
                result *= -1;
            return result;
        }
    }
}
