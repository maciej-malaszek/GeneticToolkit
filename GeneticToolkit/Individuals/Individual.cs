using GeneticToolkit.Interfaces;

using JetBrains.Annotations;

namespace GeneticToolkit.Individuals
{
    [PublicAPI]
    public class Individual<TFitnessFunctionFactory> : IIndividual where TFitnessFunctionFactory : IFitnessFunctionFactory, new()
    {
        private double? _fitnessValue;
        
        private static IFitnessFunction _fitnessFunction;
        
        public IGenotype Genotype { get; set; }

        public IPhenotype Phenotype { get; }

        public int CompareTo(IIndividual other, ICompareCriteria criteria)
        {
            return criteria.Compare(this, other);
        }

        public double Value
        {
            get
            {
                _fitnessValue ??= _fitnessFunction.GetValue(this);
                return _fitnessValue.Value;
            }
        }

        public Individual(IGenotype genotype, IPhenotype phenotype)
        {
            Genotype = genotype;
            Phenotype = phenotype;
            Phenotype.Genotype = genotype;

            if (_fitnessFunction != null)
            {
                return;
            }

            var factory = new TFitnessFunctionFactory();
            _fitnessFunction = factory.Make();
        }
        
        public Individual() {}
    }
}
