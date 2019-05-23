namespace GeneticToolkit.Interfaces
{    
    public enum EOptimizationModeOrder { Maximize, Minimize};
    public interface ICompareCriteria
    {
        EOptimizationModeOrder OptimizationMode { get; }
        IIndividual GetBetter(IIndividual x1, IIndividual x2);
        int Compare(IIndividual x1, IIndividual x2);
    }
}
