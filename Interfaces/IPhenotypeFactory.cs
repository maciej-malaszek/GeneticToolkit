namespace GeneticToolkit.Interfaces
{
    public interface IPhenotypeFactory<out TPhenotype> where TPhenotype : IPhenotype
    {
        TPhenotype Make(IGenotype genotype);
    }
}
