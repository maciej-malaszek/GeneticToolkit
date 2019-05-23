using GeneticToolkit.Interfaces;

using System;

namespace GeneticToolkit.Policies.Incompatibility
{
    /// <summary>
    /// Special type of policy that does nothing. It should be used when there is no limit or genotype is immune to incompatibilities.
    /// </summary>
    public class AllowAll : IIncompatibilityPolicy
    {
        public AllowAll()
        {
            IsCompatible = (population, individual) => true;
        }

        public Func<IPopulation, IIndividual, bool> IsCompatible { get; set; }

        public IIndividual GetReplacement(IPopulation population, IIndividual incompatibleIndividual,
            IGenotype[] parents)
        {
            return incompatibleIndividual;
        }
    }
}