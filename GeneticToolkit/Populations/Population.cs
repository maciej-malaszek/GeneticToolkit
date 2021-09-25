using GeneticToolkit.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace GeneticToolkit.Populations
{
    [PublicAPI]
    public class Population<TFitnessFunctionFactory> : PopulationBase<TFitnessFunctionFactory>, IPopulation
        where TFitnessFunctionFactory : IFitnessFunctionFactory, new()
    {
        #region Params

        public int MaxSelectionTries { get; set; } = 10;
        public ICrossover Crossover { get; set; }
        public IMutation Mutation { get; set; }
        public IIncompatibilityPolicy IncompatibilityPolicy { get; set; }
        public IMutationPolicy MutationPolicy { set; get; }
        public IPopulationResizePolicy ResizePolicy { set; get; }
        public ISelectionMethod SelectionMethod { get; set; }

        #endregion

        public Population()
        {
        }

        public Population(int size)
        {
            Individuals = new IIndividual[size];
        }

        public override void NextGeneration()
        {
            var nextGenSize = ResizePolicy.NextGenSize(this);
            var nextGeneration = new IIndividual[nextGenSize];
            for (var i = 0; i < nextGenSize / Crossover.ChildrenCount; i++)
            {
                var parentalGenotypes = SelectParentalGenotypes();
                var genotypes = Crossover.Cross(parentalGenotypes);
                for (var j = 0; j < genotypes.Length; j++)
                {
                    var child = IndividualFactory.CreateFromGenotype(genotypes[j]);
                    Mutation.Mutate(child.Genotype, MutationPolicy, this);
                    if (!IncompatibilityPolicy.IsCompatible(this, child))
                    {
                        child = IncompatibilityPolicy.GetReplacement(this, child, parentalGenotypes);
                    }

                    nextGeneration[i * Crossover.ChildrenCount + j] = child;
                }
            }

            var childrenCountDifference = nextGenSize % Crossover.ChildrenCount;
            if (childrenCountDifference > 0)
            {
                var parentalGenotypes = SelectParentalGenotypes();
                var genotypes = Crossover.Cross(parentalGenotypes);
                var start = nextGenSize - childrenCountDifference;
                for (var j = 0; j < childrenCountDifference; j++)
                {
                    var child = IndividualFactory.CreateFromGenotype(genotypes[j]);
                    Mutation.Mutate(child.Genotype, MutationPolicy, this);
                    if (!IncompatibilityPolicy.IsCompatible(this, child))
                    {
                        child = IncompatibilityPolicy.GetReplacement(this, child, parentalGenotypes);
                    }

                    nextGeneration[start + j] = child;
                }
            }


            Individuals = nextGeneration;
            DeprecateData();
            UpdatePerGenerationData();
            if (Generation % 1000 == 0)
            {
                GC.Collect();
            }
        }


        #region Private Methods

        private bool ParentAlreadySelected(IGenotype candidate, IEnumerable<IGenotype> parents)
        {
            return parents
                .Any(parent => parent != null && 
                    (candidate == parent || candidate is null || candidate.SimilarityCheck(parent) > IncestLimit)
                );
        }

        private IGenotype[] SelectParentalGenotypes()
        {
            var parents = new IGenotype[Crossover.ParentsCount];
            for (var x = 0; x < parents.Length; x++)
            {
                var trial = 0;
                var candidate = SelectionMethod.Select(this)?.Genotype;
                while (ParentAlreadySelected(candidate, parents) && Homogeneity < DegenerationLimit && trial++ < MaxSelectionTries)
                {
                    candidate = SelectionMethod.Select(this)?.Genotype;
                }

                parents[x] = candidate;
            }

            return parents;
        }

        #endregion
    }
}