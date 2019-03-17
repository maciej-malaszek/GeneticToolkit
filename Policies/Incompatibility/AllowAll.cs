using System;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Policies.Incompatibility
{
    /// <summary>
    /// Special type of policy that does nothing. It should be used when there is no limit or genotype is immune to incompatibilities.
    /// </summary>
    public class AllowAll : IIncompatibilityPolicy
    {
        public Func<IPopulation, IIndividual, bool> IsCompatible { get; set; }
        public IIndividual GetReplacement(IPopulation population, IIndividual incompatibleIndividual, IIndividual[] parents)
        {
            return incompatibleIndividual;
        }

        public AllowAll()
        {
            IsCompatible = (population, individual) => true;
        }
    }
}
