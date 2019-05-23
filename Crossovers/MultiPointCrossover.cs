using GeneticToolkit.Interfaces;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeneticToolkit.Utils.Data;

namespace GeneticToolkit.Crossovers
{
    public class MultiPointCrossover : ICrossover
    {
        public int ParentsCount { get; set; }
        public int ChildrenCount { get; set; }
        public int CutPointCount { get; set; }
        protected Random RandomNumberGenerator { get; set; } = new Random();

        public MultiPointCrossover(IDictionary<string, object> parameters)
        {
            ParentsCount =  (int) parameters["parentsCount"];
            ChildrenCount = (int) parameters["childrenCount"];
            CutPointCount = (int) parameters["cutPointCount"];
        }
        public MultiPointCrossover(int parentsCount, int childrenCount, int cutPointCount)
        {
            ParentsCount = parentsCount;
            ChildrenCount = childrenCount;
            CutPointCount = cutPointCount;
        }

        public IList<IGenotype> Cross(IList<IGenotype> parents)
        {
            return Cross(parents, ChildrenCount);
        }
        public IList<IGenotype> Cross(IList<IGenotype> parents, int childrenCount)
        {
            int genotypeLength = parents[0].Length;
            int parentIndex = 0;
            var children = new List<IGenotype>(childrenCount);
            for (int c = 0; c < childrenCount; c++)
            {
                IGenotype child = parents[0].EmptyCopy();
                List<int> cutIndexes = GetCutPoints(genotypeLength).ToList();   
                for (int i = 0; i < CutPointCount - 1; i++)
                {
                    BitArray mask = GetMask(genotypeLength, cutIndexes, i);
                    child.Genes = child.Genes.Or(mask.And(parents[parentIndex++].Genes));
                    if (parentIndex >= parents.Count)
                        parentIndex = 0;
                }
                children.Add(child);
            }
            return children;
        }
        private IEnumerable<int> GetCutPoints(int genotypeLength)
        {
            var cutIndexes = new SortedSet<int>();
            for (int i = 0; i < CutPointCount; i++)
                // Add returns true if added to sorted set and false if already exists in hashset
                while (!cutIndexes.Add(RandomNumberGenerator.Next(genotypeLength))) { }
            return cutIndexes;
        }
        private static BitArray GetMask(int genotypeLength, IList<int> cutIndexes, int index)
        {
            int startCut = 0, endCut = cutIndexes[index];
            if(index!=0)
             startCut = cutIndexes[index-1];
            var mask = new BitArray(endCut - startCut, true) { Length = genotypeLength };
            return mask.LeftShift(startCut);
        }

        public GeneticAlgorithmParameter Serialize()
        {
            return new GeneticAlgorithmParameter(this)
            {
                Params = new Dictionary<string, object>()
                {
                    { "ParentsCount", ParentsCount   },
                    { "ChildrenCount", ChildrenCount },
                    { "CutPointCount", CutPointCount }
                }
            };
        }
    }
}
