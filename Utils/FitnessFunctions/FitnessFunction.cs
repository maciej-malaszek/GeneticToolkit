using System;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Utils.FitnessFunctions
{
    public class FitnessFunction : IFitnessFunction
    {
        public Func<IPhenotype, double> Function { get; private set; }
        public FitnessFunction(Func<IPhenotype, double> function)
        {
            Function = function;
        }

        public double GetValue( IIndividual  x)
        {
            return Function(x.Phenotype);
        }

        public double GetValue(IPhenotype x)
        {
            return Function(x);
        }
       
    }
}
