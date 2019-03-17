using System;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Policies.CrossOver
{
    public class BinarySinglePoint : ICrossOverPolicy
    {
        public int CutPointsPerParent => 1;

        public int ParentsCount => 2;

        private int TotalCuts => CutPointsPerParent * ParentsCount - 1;

        private readonly Random _random = new Random();

        public int GetCutPoint(int rangeTop, int cutIndex, int previousCut)
        {
            if(cutIndex == TotalCuts)
                return rangeTop;
            int maxCutPoint = rangeTop - TotalCuts + cutIndex;
            return _random.Next(previousCut, maxCutPoint);
        }
    }
}
