using System;
using JetBrains.Annotations;

namespace GeneticToolkit.Interfaces
{
    [PublicAPI]
    public interface IFitnessFunction
    {
        Func<IPhenotype, double> Function { get; }
        double GetValue(IIndividual x);
        double GetValue(IPhenotype x);
    }
}
