namespace GeneticToolkit.Interfaces
{
    public interface IStopCondition
    {
        bool Satisfied(IPopulation population);

        void Reset();
    }
}
