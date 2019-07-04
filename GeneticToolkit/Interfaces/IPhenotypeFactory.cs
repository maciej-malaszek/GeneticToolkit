using JetBrains.Annotations;

namespace GeneticToolkit.Interfaces
{
    [PublicAPI]
    public interface IPhenotypeFactory<out TPhenotype> where TPhenotype : IPhenotype
    {
        TPhenotype Make(IGenotype genotype);
    }
}
