using System.Collections;
using System.Collections.Generic;
using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils.Data;

namespace GeneticToolkit.Policies.Resize
{
    public class ConstantResizePolicy : IPopulationResizePolicy
    {
        public ConstantResizePolicy(IDictionary<string, object> parameters)
        {
            
        }

        public ConstantResizePolicy()
        {
            
        }
        public int NextGenSize(IPopulation population)
        {
            return population.Size;
        }

        public GeneticAlgorithmParameter Serialize()
        {
            return new GeneticAlgorithmParameter(this);
        }
    }
}
