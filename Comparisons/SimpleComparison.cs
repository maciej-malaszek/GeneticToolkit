using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Comparisons
{
    public class SimpleComparison : ICompareCriteria 
    {
        public IFitnessFunction FitnessFunction { get; set; }

        public enum EOrder { Ascending, Descending};

        private EOrder Order { get; set; } = EOrder.Ascending;

        public  IIndividual GetBetter( IIndividual  x1,  IIndividual  x2)
        {
            if (x1 == null)
                return x2;
            if (x2 == null)
                return x1;
            return Compare(x1, x2) <= 0 ? x2 : x1;
        }

        public int Compare( IIndividual  x1,  IIndividual  x2)
        {
        
            int result = FitnessFunction.GetValue(x1).CompareTo(FitnessFunction.GetValue(x2));
            if (Order == EOrder.Descending)
                result *= -1;
            return result;
        }
    }
}
