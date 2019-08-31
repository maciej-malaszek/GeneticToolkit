using System;
using System.Collections;
using System.Linq;
using GeneticToolkit.Crossovers;
using GeneticToolkit.Genotypes.Primitive;
using GeneticToolkit.Interfaces;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace CrossoversNUnit
{

    public class ArithmeticCrossoverTest
    {

        private ArithmeticCrossover _singleArithmeticCrossover;
        private ArithmeticCrossover _doubleArithmeticCrossover;
        private ArithmeticCrossover _shortArithmeticCrossover;
        private ArithmeticCrossover _integerArithmeticCrossover;
        private ArithmeticCrossover _longArithmeticCrossover;

        private struct TestCase<T>
        {
            public IGenotype[] Parents { get; set; }
            public T ExpectedValue { get; set; }

        }


        [SetUp]
        public void Setup()
        {
            _singleArithmeticCrossover = new ArithmeticCrossover(ArithmeticCrossover.EMode.Single);
            _doubleArithmeticCrossover = new ArithmeticCrossover(ArithmeticCrossover.EMode.Double);
            _shortArithmeticCrossover = new ArithmeticCrossover(ArithmeticCrossover.EMode.Short);
            _integerArithmeticCrossover = new ArithmeticCrossover(ArithmeticCrossover.EMode.Integer);
            _longArithmeticCrossover = new ArithmeticCrossover(ArithmeticCrossover.EMode.Long);

            
        }

        [Test]
        public void PrimitiveFloat32()
        {
            TestCase<float> testCase001 = new TestCase<float>()
            {
                Parents = new[]
                {
                    new Float32Genotype(0.0f),
                    new Float32Genotype(0.0f),
                },
                ExpectedValue = 0
            };

            TestCase<float> testCase002 = new TestCase<float>()
            {
                Parents = new[]
                {
                    new Float32Genotype(0.0f),
                    new Float32Genotype((float)new Random().NextDouble()*10000+1),
                },
            };
            testCase002.ExpectedValue = testCase002.Parents.Average(x => ((Float32Genotype) x).Value);

            TestCase<float> testCase003 = new TestCase<float>()
            {
                Parents = new[]
                {
                    new Float32Genotype(11.0f),
                    new Float32Genotype(12.0f),
                },
            };
            testCase003.ExpectedValue = 11.5f;

            TestCase<float> testCase004 = new TestCase<float>()
            {
                Parents = new[]
                {
                    new Float32Genotype(-(float)new Random().NextDouble()*10000-1),
                    new Float32Genotype((float)new Random().NextDouble()*10000+1),
                },
            };
            testCase004.ExpectedValue = testCase004.Parents.Average(x => ((Float32Genotype)x).Value);

            Assert.AreEqual(true, GenericPrimitiveGenotypeTest(_singleArithmeticCrossover, testCase001));
            Assert.AreEqual(true, GenericPrimitiveGenotypeTest(_singleArithmeticCrossover, testCase002));
            Assert.AreEqual(true, GenericPrimitiveGenotypeTest(_singleArithmeticCrossover, testCase003));
            Assert.AreEqual(true, GenericPrimitiveGenotypeTest(_singleArithmeticCrossover, testCase004));
            Assert.Pass();
        }


        #region PrimitiveGenotypes

        private bool GenericPrimitiveGenotypeTest<T>(ArithmeticCrossover crossover, TestCase<T> testCase) where T : struct
        {
            int longestGenotype = testCase.Parents.Select(x => x.Length).Max();
            int shortestGenotype = testCase.Parents.Select(x => x.Length).Min();
            Assert.AreEqual(longestGenotype, shortestGenotype);

            int expectedGenotypeLength = testCase.Parents[0].Length;

            IGenotype[] children = crossover.Cross(testCase.Parents);

            // Arithmetic Crossover must return only one child
            Assert.AreEqual(1, children.Length);

            IGenotype childGenotype = children[0];

            Assert.NotNull(childGenotype);

            GenericPrimitiveGenotype<T> typedGenotype = childGenotype as GenericPrimitiveGenotype<T>;
            Assert.NotNull(typedGenotype);

            Assert.AreEqual(expectedGenotypeLength, typedGenotype.Length);

            Assert.AreEqual(testCase.ExpectedValue, typedGenotype.Value);

            return true;
        }

        #endregion


        #region CollectiveGenotypes



        #endregion
    }
}