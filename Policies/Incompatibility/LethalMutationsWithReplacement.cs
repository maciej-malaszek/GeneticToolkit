using System;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Policies.Incompatibility
{
    /// <summary>
    /// This is safer version of LethalMutations that will not lead to fast extinction.
    /// It may result with long loops until it will find compatible individual.
    /// If maximum retry number is small, it may lead to same result as LethalMutations.
    /// Should not be used on sets with large search space with little allowed solution space.
    /// </summary>
    public class LethalMutationsWithReplacement<TFitness> : IIncompatibilityPolicy<TFitness> where TFitness:IComparable
    {
        public Func<IPopulation<TFitness>,  IIndividual<TFitness> , bool> IsCompatible { get; set; }
        private readonly int _maxRetries;

        public  IIndividual<TFitness>  GetReplacement(IPopulation<TFitness> population,  IIndividual<TFitness>  incompatibleIndividual,  IIndividual<TFitness> [] parents)
        {
            int retry = 0;
            bool compatible;
             IIndividual<TFitness>  candidate;
            do
            {
                candidate = parents[0].CrossOver(population.CrossOverPolicy, parents);
                candidate.Mutate(population.MutationPolicy);
                retry++;
            } while((compatible = IsCompatible(population, candidate)) == false && retry < _maxRetries);

            return compatible ? candidate : null;
        }

        public LethalMutationsWithReplacement(int maxRetries,
            Func<IPopulation<TFitness>,  IIndividual<TFitness> , bool> compatibilityFunction)
        {
            _maxRetries = maxRetries;
            IsCompatible = compatibilityFunction;
        }
    }
}
