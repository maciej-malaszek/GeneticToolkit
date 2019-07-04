using GeneticToolkit.Interfaces;

using System;
using JetBrains.Annotations;

namespace GeneticToolkit.Policies.Incompatibility
{
    /// <summary>
    /// If Individual is not accepted, it will be killed and new population will be reduced.
    /// As it may lead to extinction of entire population, it is unsafe way (may crash system)
    /// Should not be used on sets with large search space with little allowed solution space.
    /// </summary>
    [PublicAPI]
    public class LethalMutation : IIncompatibilityPolicy
    {
        public Func<IPopulation, IIndividual, bool> IsCompatible { get; set; }
        public IIndividual GetReplacement(IPopulation population, IIndividual incompatibleIndividual, IGenotype[] parents)
        {
            return null;
        }
    }
}
