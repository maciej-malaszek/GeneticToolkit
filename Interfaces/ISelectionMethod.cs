namespace GeneticToolkit.Interfaces
{
    public interface ISelectionMethod : IGeneticSerializable
    {
        ICompareCriteria CompareCriteria { get; set; }

        IIndividual Select(IPopulation population);
    }
}
