using System;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Comparisons
{
    public class SimpleComparison<TFitness> : ICompareCriteria<TFitness> where TFitness : IComparable
    {
        public IFitnessFunction<TFitness> FitnessFunction { get; set; }

        public enum EOrder { Ascending, Descending};

        private EOrder Order { get; set; } = EOrder.Ascending;

        public  IIndividual<TFitness> GetBetter( IIndividual<TFitness>  x1,  IIndividual<TFitness>  x2)
        {
            if (x1 == null)
                return x2;
            if (x2 == null)
                return x1;
            return Compare(x1, x2) <= 0 ? x1 : x2;
        }

        public int Compare( IIndividual<TFitness>  x1,  IIndividual<TFitness>  x2)
        {
        
            int result = FitnessFunction.GetValue(x1).CompareTo(FitnessFunction.GetValue(x2));
            if (Order == EOrder.Descending)
                result *= -1;
            return result;
        }
    }
}
