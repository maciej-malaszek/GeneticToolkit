using GeneticToolkit.Utils;

namespace GeneticToolkit.Interfaces
{
    public interface IScaledPhenotype<T> : IPhenotype
    {
        Range<T> Scale { get; set; }

        T Length { get; }

        T Step { get; }

    }
}
