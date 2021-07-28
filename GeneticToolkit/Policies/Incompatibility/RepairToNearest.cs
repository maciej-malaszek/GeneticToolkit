using GeneticToolkit.Interfaces;
using System;
using JetBrains.Annotations;

namespace GeneticToolkit.Policies.Incompatibility
{
    [PublicAPI]
    public class RepairToNearest<TCompatibilityFunctionFactory> : IIncompatibilityPolicy
        where TCompatibilityFunctionFactory : ICompatibilityFunctionFactory, new()
    {
        public Func<IPopulation, IIndividual, bool> IsCompatible { get; set; }

        public Action<IGenotype> RepairFunction { get; set; }

        public RepairToNearest()
        {
            IsCompatible = new TCompatibilityFunctionFactory().Make();
        }

        public IIndividual GetReplacement(IPopulation population, IIndividual incompatibleIndividual, IGenotype[] parents)
        {
            RepairFunction(incompatibleIndividual.Genotype);
            return incompatibleIndividual;
        }
    }
}