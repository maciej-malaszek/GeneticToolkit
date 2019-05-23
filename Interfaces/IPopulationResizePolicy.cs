namespace GeneticToolkit.Interfaces
{
    public interface IPopulationResizePolicy : IGeneticSerializable
    {
        int NextGenSize(IPopulation population);
    }
}
