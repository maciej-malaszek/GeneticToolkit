using System;
using GeneticToolkit.Factories;
using GeneticToolkit.Genotypes.Collective;
using GeneticToolkit.Interfaces;
using GeneticToolkit.Phenotypes.Collective;
using NUnit.Framework;

namespace GeneticToolkit.UnitTests.Phenotypes
{
    internal struct SimpleStructure : IGeneticallySerializable
    {
        public int IntegerValue { get; set; }

        public byte[] Serialize()
        {
            return BitConverter.GetBytes(IntegerValue);
        }

        public IGeneticallySerializable Deserialize(byte[] data)
        {
            IntegerValue = BitConverter.ToInt32(data);
            return this;
        }
    }

    public class CollectivePhenotypeTests
    {
        private CollectivePhenotypeFactory<SimpleStructure> _phenotypeFactory;

        private IndividualFactory<CollectiveGenotype<SimpleStructure>, CollectivePhenotype<SimpleStructure>, MockFitnessFunctionFactory>
            _individualFactory;

        private IIndividual _individual;

        private const int TestedInteger = 1234;

        private static readonly SimpleStructure _testedSimpleStructure = new()
            {IntegerValue = TestedInteger};

        [SetUp]
        public void Setup()
        {
            _phenotypeFactory = new CollectivePhenotypeFactory<SimpleStructure>();
            _individualFactory =
                new IndividualFactory<CollectiveGenotype<SimpleStructure>, CollectivePhenotype<SimpleStructure>, MockFitnessFunctionFactory>(
                    _phenotypeFactory);
        }

        [Test(Author = "Maciej Małaszek", Description = "Is it possible to create individual with such phenotype")]
        public void Creation()
        {
            _individual = _individualFactory.CreateRandomIndividual();

            Assert.IsNotNull(_individual);

            Assert.IsNotNull(_individual.Genotype);

            Assert.AreEqual(_individual.Genotype.Length, sizeof(int));

            Assert.IsNotNull(_individual.Phenotype);

            Assert.IsAssignableFrom(typeof(CollectivePhenotype<SimpleStructure>), _individual.Phenotype);

            Assert.Pass();
        }


        [Test(Author = "Maciej Małaszek", Description = "Is phenotype correctly decoded")]
        public void Decoding()
        {
            var stubGenotype = new CollectiveGenotype<SimpleStructure>(_testedSimpleStructure);
            var collectivePhenotype = _phenotypeFactory.Make(stubGenotype);
            var decodedStructure = collectivePhenotype.GetValue();

            Assert.IsNotNull(decodedStructure);

            Assert.AreEqual(decodedStructure.IntegerValue, TestedInteger);

            Assert.Pass();
        }
    }
}