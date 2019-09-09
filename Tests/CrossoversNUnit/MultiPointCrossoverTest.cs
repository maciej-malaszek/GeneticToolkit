using GeneticToolkit.Crossovers;
using GeneticToolkit.Genotypes.Primitive;
using GeneticToolkit.Interfaces;

using NUnit.Framework;

using System;
using System.Linq;

namespace CrossoversNUnit
{
    public class MultiPointCrossoverTest
    {

        private MultiPointCrossover _multiPointCrossover;

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
            _multiPointCrossover = new MultiPointCrossover(parentsCount, childrenCount, cutPoints) {BitAlign = bitAlign};
            IGenotype[] children = _multiPointCrossover.Cross(_parents.Take(parentsCount).ToArray());

            Assert.NotNull(children);
            Assert.AreEqual(childrenCount, children.Length);
            foreach (IGenotype genotype in children)
            {
                Assert.NotNull(genotype);
            }
            Assert.Pass();
        }

        //[Test(Description = "2 parents, 1 child, 1 cutpoint, bit align to 1")]
        //public void TwoParentsSingleChild()
        //{
        //    const int childrenCount = 1;
        //    const int parentsCount = 2;
            
        //    PerformTest(childrenCount, parentsCount, 1, 1);
        //    PerformTest(childrenCount, parentsCount, 2, 1);
        //    PerformTest(childrenCount, parentsCount, 1, 2);
        //    PerformTest(childrenCount, parentsCount, 4, 1);
        //    Assert.Pass();
        //}

    }
}
