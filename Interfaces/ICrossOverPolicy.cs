using System;

namespace GeneticToolkit.Interfaces
{
    public interface ICrossOverPolicy<TFitness> where TFitness:IComparable
    {
        int CutPointsPerParent { get; }

        int ParentsCount { get; }

        int GetCutPoint(int rangeTop, int cutIndex, int previousCut);
    }
}
