using GeneticToolkit.Utils;
using JetBrains.Annotations;

namespace GeneticToolkit.Interfaces
{
    [PublicAPI]
    public interface IScaledPhenotype<T> : IPhenotype
    {
        Range<T> Scale { get; set; }

        T Length { get; }

        T Step { get; }

    }
}
