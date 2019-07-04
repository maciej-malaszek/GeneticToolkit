using JetBrains.Annotations;

namespace GeneticToolkit.Interfaces
{
    [PublicAPI]
    public interface IPhenotype
    {
        IGenotype Genotype { get; set; }

        IPhenotype ShallowCopy();
    }
}
