namespace GeneticToolkit.Interfaces
{
    public interface IStatisticUtility
    {
        int Length { get; }
        double GetValue(int generation);
        void UpdateData(IEvolutionaryPopulation population);

        void Reset();
    }
}
