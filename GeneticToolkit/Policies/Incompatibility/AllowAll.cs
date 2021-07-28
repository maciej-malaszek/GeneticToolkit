using GeneticToolkit.Interfaces;

using System;
using JetBrains.Annotations;

namespace GeneticToolkit.Policies.Incompatibility
{
    /// <summary>
    /// Special type of policy that does nothing. It should be used when there is no limit or genotype is immune to incompatibilities.
    /// </summary>
    [PublicAPI]
    public class AllowAll : IIncompatibilityPolicy
    {
        public AllowAll()
        {
            IsCompatible = (_, _) => true;
        }

        public Func<IPopulation, IIndividual, bool> IsCompatible { get; set; }

        public IIndividual GetReplacement(IPopulation population, IIndividual incompatibleIndividual,
            IGenotype[] parents)
        {
            return incompatibleIndividual;
        }
    }
}