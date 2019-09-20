using GeneticToolkit.Interfaces;

using JetBrains.Annotations;

namespace GeneticToolkit.Individuals
{
    [PublicAPI]
    public class Individual : IIndividual
    {
        private double? _fitnessValue;
        public IGenotype Genotype { get; set; }

        public IPhenotype Phenotype { get; }

        public int CompareTo(IIndividual other, ICompareCriteria criteria)
        {
            return criteria.Compare(this, other);
        }

        public double Value
        {
            get {
                if (!_fitnessValue.HasValue)
                    _fitnessValue = FitnessFunction.GetValue(this);
                return _fitnessValue.Value;
            }
        }

        public IFitnessFunction FitnessFunction { get; set; }

        public Individual(IGenotype genotype, IPhenotype phenotype, IFitnessFunction fitnessFunction)
        {
            Genotype = genotype;
            Phenotype = phenotype;
            Phenotype.Genotype = genotype;
            FitnessFunction = fitnessFunction;
        }
    }
}
