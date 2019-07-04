namespace GeneticToolkit.Interfaces
{    
    public enum EOptimizationMode { Maximize, Minimize};
    public interface ICompareCriteria
    {
        EOptimizationMode OptimizationMode { get; }
        IIndividual GetBetter(IIndividual x1, IIndividual x2);
        int Compare(IIndividual x1, IIndividual x2);
    }
}
