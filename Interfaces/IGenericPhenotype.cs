namespace GeneticToolkit.Interfaces
{
    public interface IGenericPhenotype<out T> : IPhenotype
    {
        T GetValue();
    }
}
