using System;
using System.Linq;
using GeneticToolkit.Crossovers;
using GeneticToolkit.Genotypes.Primitive;
using NUnit.Framework;

namespace GeneticToolkit.UnitTests.Crossovers
{
    public class UniformCrossoverTest
    {
        private UniformCrossover _uniformCrossover;

        private PrimitiveGenotype[] _parents;

        [SetUp]
        public void Setup()
        {
            _parents = new PrimitiveGenotype[4];
            for (var i = 0; i < _parents.Length; i++)
            {
                _parents[i] = new UInt64Genotype((ulong)new Random().Next());
            }
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
            var children = _uniformCrossover.Cross(_parents.Take(parentsCount).ToArray());

            Assert.NotNull(children);
            Assert.AreEqual(childrenCount, children.Length);
            foreach (var genotype in children)
            {
                Assert.NotNull(genotype);
            }
            Assert.Pass();
        }

    }
}
