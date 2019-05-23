using GeneticToolkit.Interfaces;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using GeneticToolkit.Utils.Data;

namespace GeneticToolkit.Crossovers
{
    public class SinglePointCrossover : ICrossover
    {
        protected Random RandomNumberGenerator { get; set; } = new Random();
        public int ParentsCount { get; } = 2;
        public int ChildrenCount { get; } = 2;

        public SinglePointCrossover() { }
        public SinglePointCrossover(IDictionary<string, object> parameters) { }

        public IList<IGenotype> Cross(IList<IGenotype> parents)
        {
            int genotypeLength = parents[0].Length;
            IGenotype[] children = { parents[0].ShallowCopy(), parents[1].ShallowCopy() };

            var cutIndex = RandomNumberGenerator.Next(genotypeLength);
            var maskArray = new BitArray(genotypeLength, true);
            maskArray.LeftShift(cutIndex);

            children[0].Genes = children[0].Genes.Or(maskArray.And(parents[1].Genes)); 
            children[1].Genes = children[1].Genes.Or(maskArray.And(parents[0].Genes));

            return children;
        }

        public GeneticAlgorithmParameter Serialize()
        {
            return new GeneticAlgorithmParameter(this);
        }
    }
}
