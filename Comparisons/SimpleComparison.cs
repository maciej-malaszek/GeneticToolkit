using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Comparisons
{
    public class SimpleComparison : ICompareCriteria 
    {
        public enum EOrder { Maximize, Minimize};

        public IFitnessFunction FitnessFunction { get; set; }

        private EOrder Order { get; set; }

        public SimpleComparison(IFitnessFunction fitnessFunction, EOrder order = EOrder.Maximize)
        {
            Order = order;
            FitnessFunction = fitnessFunction;
        }

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
            if (Order == EOrder.Maximize)
                result *= -1;
            return result;
        }
    }
}
