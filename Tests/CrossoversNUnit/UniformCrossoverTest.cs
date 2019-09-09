using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticToolkit.Crossovers;
using GeneticToolkit.Genotypes.Primitive;
using GeneticToolkit.Interfaces;
using NUnit.Framework;

namespace CrossoversNUnit
{
    public class UniformCrossoverTest
    {
        private UniformCrossover _uniformCrossover;

        private PrimitiveGenotype[] _parents;

        [SetUp]
        public void Setup()
        {
            _parents = new PrimitiveGenotype[4];
            for (int i = 0; i < _parents.Length; i++)
                _parents[i] = new UInt64Genotype((ulong)new Random().Next());
        }

        [Test]
        [TestCase(1,2,1,1)]
        [TestCase(2,2,1,1)]
        [TestCase(1,4,1,1)]
        [TestCase(4,4,1,1)]
        [TestCase(1,2,2,1)]
        [TestCase(2,2,2,1)]
        [TestCase(1,4,4,1)]
        [TestCase(4,4,4,1)]
        [TestCase(4,4,4,8)]
        public void PerformTest(int childrenCount, int parentsCount, int cutPoints, int bitAlign)
        {
            _uniformCrossover = new UniformCrossover(){BitAlign = 1, ChildrenCount = childrenCount, ParentsCount = parentsCount};
            IGenotype[] children = _uniformCrossover.Cross(_parents.Take(parentsCount).ToArray());

            Assert.NotNull(children);
            Assert.AreEqual(childrenCount, children.Length);
            foreach (IGenotype genotype in children)
            {
                Assert.NotNull(genotype);
            }
            Assert.Pass();
        }

    }
}
