using System;
using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils.Data;

namespace GeneticToolkit.Policies.Incompatibility
{
    public class RepairToNearest : IIncompatibilityPolicy
    {
        public Func<IPopulation, IIndividual, bool> IsCompatible { get; set; }

        public Action<IGenotype> RepairFunction { get; set; }


        public IIndividual GetReplacement(IPopulation population, IIndividual incompatibleIndividual, IIndividual[] parents)
        {
            RepairFunction(incompatibleIndividual.Genotype);
            return incompatibleIndividual;
        }

        public GeneticAlgorithmParameter Serialize()
        {
            return new GeneticAlgorithmParameter(this);
        }
    }
}
