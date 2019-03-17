using System;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Policies.Incompatibility
{
    /// <summary>
    /// If Individual is not accepted, it will be killed and new population will be reduced.
    /// As it may lead to extinction of entire population, it is unsafe way (may crash system)
    /// Should not be used on sets with large search space with little allowed solution space.
    /// </summary>
    public class LethalMutation : IIncompatibilityPolicy
    {
        public Func<IPopulation, IIndividual, bool> IsCompatible { get; set; }
        public IIndividual GetReplacement(IPopulation population, IIndividual incompatibleIndividual, IIndividual[] parents)
        {
            return null;
        }
    }
}
