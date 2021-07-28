using GeneticToolkit.FitnessFunctions;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.UnitTests
{
    public class MockFitnessFunctionFactory : IFitnessFunctionFactory
    {
        private static readonly IFitnessFunction _fitnessFunction = new FitnessFunction(_ => 0);

        public IFitnessFunction Make()
        {
            return _fitnessFunction;
        }
    }
}