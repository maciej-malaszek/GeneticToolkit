using GeneticToolkit.Interfaces;
using JetBrains.Annotations;

namespace GeneticToolkit.Policies.Resize
{
    [PublicAPI]
    public class ConstantResizePolicy : IPopulationResizePolicy
    {
        public int NextGenSize(IPopulation population)
        {
            return population.Size;
        }
    }
}