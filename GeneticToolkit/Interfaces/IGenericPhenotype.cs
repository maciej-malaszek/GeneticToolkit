using JetBrains.Annotations;

namespace GeneticToolkit.Interfaces
{
    [PublicAPI]
    public interface IGenericPhenotype<out T> : IPhenotype
    {
        T GetValue();
    }
}
