using JetBrains.Annotations;

namespace GeneticToolkit.Interfaces
{
    [PublicAPI]
    public interface ISelectionMethod
    {
        ICompareCriteria CompareCriteria { get; set; }

        IIndividual Select(IPopulation population);
    }
}
