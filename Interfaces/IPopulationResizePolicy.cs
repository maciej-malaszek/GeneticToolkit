namespace GeneticToolkit.Interfaces
{
    public interface IPopulationResizePolicy
    {
        int NextGenSize(IPopulation population);
    }
}
