namespace GeneticToolkit.Interfaces
{    
    public interface ICompareCriteria
    {
        IIndividual GetBetter(IIndividual x1, IIndividual x2);
        int Compare(IIndividual x1, IIndividual x2);
    }
}
