namespace GeneticToolkit.Interfaces
{
    public interface IPhenotypeFactory<TPhenotype> where TPhenotype : IPhenotype
    {
        TPhenotype Make(IGenotype genotype);
    }
}
