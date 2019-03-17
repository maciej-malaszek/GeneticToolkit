namespace GeneticToolkit.Interfaces
{
    public interface ICrossOverPolicy
    {
        int CutPointsPerParent { get; }

        int ParentsCount { get; }

        int GetCutPoint(int rangeTop, int cutIndex, int previousCut);
    }
}
