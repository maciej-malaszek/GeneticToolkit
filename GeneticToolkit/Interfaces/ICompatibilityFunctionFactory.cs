using System;

namespace GeneticToolkit.Interfaces
{
    public interface ICompatibilityFunctionFactory
    {
        Func<IPopulation, IIndividual, bool> Make();
    }
}