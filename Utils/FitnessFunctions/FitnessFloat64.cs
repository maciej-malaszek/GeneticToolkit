using System;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Utils.FitnessFunctions
{
    public class FitnessFloat64 : FitnessFunction<double>
    {
        protected Random RandomNumberGenerator { get; set; } = new Random();
        public FitnessFloat64(Func<IPhenotype, double> function) : base(function)
        {
        }

        public override double Add(double l, double r) => l + r;
        
        public override double Subtract(double l, double r) => l - r;
        
        public override double Divide(double l, double r) => l / r;
        
        public override double Multiply(double l, double r) => l * r;
        public override double Abs(double x) => Math.Abs(x);
        public override double Random() => RandomNumberGenerator.NextDouble();

        public override double Random(double max) => RandomNumberGenerator.NextDouble() * max;

        public override double Random(double min, double max) => min + RandomNumberGenerator.NextDouble() * (max - min);
    }
}
