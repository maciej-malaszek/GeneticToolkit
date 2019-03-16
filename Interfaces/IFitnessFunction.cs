using System;

namespace GeneticToolkit.Interfaces
{
    public interface IFitnessFunction<TOutput> : IArithmetical<TOutput> where TOutput : IComparable
    {
        Func<IPhenotype, TOutput> Function { get; } 
        TOutput GetValue( IIndividual<TOutput> x);
        TOutput GetValue(IPhenotype x);
    }
}
