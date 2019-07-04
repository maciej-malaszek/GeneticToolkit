using GeneticToolkit.Interfaces;

using System;
using JetBrains.Annotations;

namespace GeneticToolkit.Policies.Incompatibility
{
    [PublicAPI]
    public class RepairToNearest : IIncompatibilityPolicy
    {
        public Func<IPopulation, IIndividual, bool> IsCompatible { get; set; }

        public Action<IGenotype> RepairFunction { get; set; }


        public IIndividual GetReplacement(IPopulation population, IIndividual incompatibleIndividual, IGenotype[] parents)
        {
            RepairFunction(incompatibleIndividual.Genotype);
            return incompatibleIndividual;
        }
    }
}
