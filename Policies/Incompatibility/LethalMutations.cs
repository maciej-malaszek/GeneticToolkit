using System;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Policies.Incompatibility
{
    /// <summary>
    /// If Individual is not accepted, it will be killed and new population will be reduced.
    /// As it may lead to extinction of entire population, it is unsafe way (may crash system)
    /// Should not be used on sets with large search space with little allowed solution space.
    /// </summary>
    public class LethalMutation<TFitness> : IIncompatibilityPolicy<TFitness> where TFitness:IComparable
    {
        public Func<IPopulation<TFitness>,  IIndividual<TFitness> , bool> IsCompatible { get; set; }
        public  IIndividual<TFitness>  GetReplacement(IPopulation<TFitness> population,  IIndividual<TFitness>  incompatibleIndividual,  IIndividual<TFitness> [] parents)
        {
            return null;
        }
    }
}
