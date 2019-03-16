using System;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Utils.FitnessFunctions
{
    public abstract class FitnessFunction<TOutput> : IFitnessFunction<TOutput> where TOutput : IComparable
    {
        public Func<IPhenotype, TOutput> Function { get; private set; }
        protected FitnessFunction(Func<IPhenotype, TOutput> function)
        {
            Function = function;
        }

        public TOutput GetValue( IIndividual<TOutput>  x)
        {
            return Function(x.Phenotype);
        }

        public TOutput GetValue(IPhenotype x)
        {
            return Function(x);
        }

        public abstract TOutput Add(TOutput l, TOutput r);
        public abstract TOutput Subtract(TOutput l, TOutput r);
        public abstract TOutput Divide(TOutput l, TOutput r);
        public abstract TOutput Multiply(TOutput l, TOutput r);
        public abstract TOutput Abs(TOutput x);

        public abstract TOutput Random();
        public abstract TOutput Random(TOutput max);
        public abstract TOutput Random(TOutput min, TOutput max);
    }
}
