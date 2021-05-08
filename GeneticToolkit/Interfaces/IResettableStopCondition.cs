namespace GeneticToolkit.Interfaces
{
    public interface IResettableStopCondition : IStopCondition
    {
        void Reset();
    }
}