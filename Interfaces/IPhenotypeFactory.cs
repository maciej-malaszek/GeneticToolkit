namespace GeneticToolkit.Interfaces
{
    public interface IPhenotypeFactory<out TPhenotype> : IGeneticSerializable where TPhenotype : IPhenotype
    {
        TPhenotype Make(IGenotype genotype);
    }
}
