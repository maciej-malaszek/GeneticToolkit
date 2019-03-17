namespace GeneticToolkit.Interfaces
{
    public interface IPhenotype
    {
        IGenotype Genotype { get; set; }

        IPhenotype ShallowCopy();
    }
}
