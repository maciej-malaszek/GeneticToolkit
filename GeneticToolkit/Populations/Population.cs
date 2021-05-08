using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils.Factories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace GeneticToolkit.Populations
{
    [PublicAPI]
    public class Population : PopulationBase, IPopulation
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
        
        public Population(IFitnessFunction fitnessFunction, int size)
        {
            FitnessFunction = fitnessFunction;
            Individuals = new IIndividual[size];
        }

        public override void NextGeneration()
        {
            int nextGenSize = ResizePolicy.NextGenSize(this);
            var nextGeneration = new IIndividual[nextGenSize];
            for (var i = 0; i < nextGenSize / Crossover.ChildrenCount; i++)
            {
                IGenotype[] parentalGenotypes = SelectParentalGenotypes();
                IGenotype[] genotypes = Crossover.Cross(parentalGenotypes);
                for (var j = 0; j < genotypes.Length; j++)
                {
                    IIndividual child = IndividualFactory.CreateFromGenotype(genotypes[j]);
                    Mutation.Mutate(child.Genotype, MutationPolicy, this);
                    if (!IncompatibilityPolicy.IsCompatible(this, child))
                        child = IncompatibilityPolicy.GetReplacement(this, child, parentalGenotypes);
                    nextGeneration[i * Crossover.ChildrenCount + j] = child;
                }
            }

            int childrenCountDifference = nextGenSize % Crossover.ChildrenCount;
            if (childrenCountDifference > 0)
            {
                IGenotype[] parentalGenotypes = SelectParentalGenotypes();
                IGenotype[] genotypes = Crossover.Cross(parentalGenotypes);
                int start = nextGenSize - childrenCountDifference;
                for (var j = 0; j < childrenCountDifference; j++)
                {
                    IIndividual child = IndividualFactory.CreateFromGenotype(genotypes[j]);
                    Mutation.Mutate(child.Genotype, MutationPolicy, this);
                    if (!IncompatibilityPolicy.IsCompatible(this, child))
                        child = IncompatibilityPolicy.GetReplacement(this, child, parentalGenotypes);
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
            return parents.Any(parent =>
                parent != null && (candidate == parent || candidate.SimilarityCheck(parent) > IncestLimit));
        }

        private IGenotype[] SelectParentalGenotypes()
        {
            var parents = new IGenotype[Crossover.ParentsCount];
            for (var x = 0; x < parents.Length; x++)
            {
                var trial = 0;
                IGenotype candidate = SelectionMethod.Select(this).Genotype;
                while (ParentAlreadySelected(candidate, parents) && Homogeneity < DegenerationLimit &&
                       trial++ < MaxSelectionTries)
                    candidate = SelectionMethod.Select(this).Genotype;
                parents[x] = candidate;
            }

            return parents;
        }


        #endregion


    }
}