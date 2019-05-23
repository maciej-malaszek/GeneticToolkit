using System;
using System.Collections.Generic;
using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils.Data;

namespace GeneticToolkit.Policies.Incompatibility
{
    /// <summary>
    /// Special type of policy that does nothing. It should be used when there is no limit or genotype is immune to incompatibilities.
    /// </summary>
    public class AllowAll : IIncompatibilityPolicy
    {
        public Func<IPopulation, IIndividual, bool> IsCompatible { get; set; }
        public IIndividual GetReplacement(IPopulation population, IIndividual incompatibleIndividual, IIndividual[] parents)
        {
            return incompatibleIndividual;
        }

        public AllowAll(IDictionary<string, object> parameters)
        {
            
        }

        public AllowAll()
        {
            IsCompatible = (population, individual) => true;
        }

        public GeneticAlgorithmParameter Serialize()
        {
            return new GeneticAlgorithmParameter(this);
        }
    }
}
