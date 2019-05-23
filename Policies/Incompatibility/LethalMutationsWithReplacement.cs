using System;
using System.Linq;
using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils.Data;

namespace GeneticToolkit.Policies.Incompatibility
{
    /// <summary>
    /// This is safer version of LethalMutations that will not lead to fast extinction.
    /// It may result with long loops until it will find compatible individual.
    /// If maximum retry number is small, it may lead to same result as LethalMutations.
    /// Should not be used on sets with large search space with little allowed solution space.
    /// </summary>
    public class LethalMutationsWithReplacement : IIncompatibilityPolicy
    {
        public Func<IPopulation, IIndividual, bool> IsCompatible { get; set; }
        private readonly int _maxRetries;

        public IIndividual GetReplacement(IPopulation population, IIndividual incompatibleIndividual, IIndividual[] parents)
        {
            int retry = 0;
            bool compatible;
            IIndividual candidate;
            do
            {
                candidate = population.IndividualFactory.CreateFromGenotype(
                    population.Crossover.Cross(parents.Select(x => x.Genotype).ToList()).First(),
                    incompatibleIndividual.Phenotype.ShallowCopy());
                candidate.Mutate(population.MutationPolicy);
                retry++;
            } while((compatible = IsCompatible(population, candidate)) == false && retry < _maxRetries);

            return compatible ? candidate : null;
        }

        public LethalMutationsWithReplacement(int maxRetries,
            Func<IPopulation, IIndividual, bool> compatibilityFunction)
        {
            _maxRetries = maxRetries;
            IsCompatible = compatibilityFunction;
        }

        public GeneticAlgorithmParameter Serialize()
        {
            return new GeneticAlgorithmParameter(this);
        }
    }
}
