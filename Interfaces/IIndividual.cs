namespace GeneticToolkit.Interfaces
{
    public interface IIndividual
    {
        IGenotype Genotype { get; set; }

        IPhenotype Phenotype { get; }

        void Mutate(IMutationPolicy policy);

        int CompareTo(IIndividual other, ICompareCriteria criteria);

        IIndividual CrossOver(ICrossOverPolicy policy, params IIndividual[] individuals);
    }
}
