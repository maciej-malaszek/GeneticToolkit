namespace GeneticToolkit.Interfaces
{
    public interface ICompareCriteria
    {
        IFitnessFunction FitnessFunction { get; set; }
        IIndividual GetBetter(IIndividual x1, IIndividual x2);
        int Compare(IIndividual x1, IIndividual x2);
    }
}
