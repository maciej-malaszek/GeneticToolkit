using GeneticToolkit.Interfaces;
using System;
using JetBrains.Annotations;

namespace GeneticToolkit.Policies.Incompatibility
{
    /// <summary>
    /// This is safer version of LethalMutations that will not lead to fast extinction.
    /// It may result with long loops until it will find compatible individual.
    /// If maximum retry number is small, it may lead to same result as LethalMutations.
    /// Should not be used on sets with large search space with little allowed solution space.
    /// </summary>
    [PublicAPI]
    public class LethalMutationsWithReplacement : IIncompatibilityPolicy
    {
        public Func<IPopulation, IIndividual, bool> IsCompatible { get; set; }
        private readonly int _maxRetries;

        public IIndividual GetReplacement(IPopulation population, IIndividual incompatibleIndividual,
            IGenotype[] parents)
        {
            var retry = 0;
            bool compatible;
            IIndividual candidate;
            do
            {
                candidate = population.IndividualFactory.CreateFromGenotype(
                    population.Crossover.Cross(parents)[0]
                );
                population.Mutation.Mutate(candidate.Genotype, population.MutationPolicy, population);
                retry++;
                compatible = IsCompatible(population, candidate);
            } while (!compatible && retry < _maxRetries);

            return compatible ? candidate : null;
        }

        public LethalMutationsWithReplacement(int maxRetries,
            Func<IPopulation, IIndividual, bool> compatibilityFunction)
        {
            _maxRetries = maxRetries;
            IsCompatible = compatibilityFunction;
        }
        
        public LethalMutationsWithReplacement() {}
    }
}