namespace GeneticToolkit.Interfaces
{
    public interface ISelectionMethod
    {
        ICompareCriteria CompareCriteria { get; set; }

        IIndividual Select(IPopulation population);
    }
}
