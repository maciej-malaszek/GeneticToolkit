using System;

namespace GeneticToolkit.Interfaces
{
    public interface IFitnessFunction
    {
        Func<IPhenotype, double> Function { get; }
        double GetValue(IIndividual x);
        double GetValue(IPhenotype x);
    }
}
