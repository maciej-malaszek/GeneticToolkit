namespace GeneticToolkit.Interfaces
{
    public interface IIndividual
    {
        IGenotype Genotype { get; set; }

        IPhenotype Phenotype { get; }

        int CompareTo(IIndividual other, ICompareCriteria criteria);
    }
}
