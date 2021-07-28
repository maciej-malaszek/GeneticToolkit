using System;

namespace GeneticToolkit.Interfaces
{
    public interface IRankingFunctionFactory
    {
        Func<int, double> Make();
    }
}