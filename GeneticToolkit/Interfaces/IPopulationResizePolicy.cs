using JetBrains.Annotations;

namespace GeneticToolkit.Interfaces
{
    [PublicAPI]
    public interface IPopulationResizePolicy
    {
        int NextGenSize(IPopulation population);
    }
}
