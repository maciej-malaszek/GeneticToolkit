using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using GeneticToolkit.Comparisons;
using GeneticToolkit.Crossovers;
using GeneticToolkit.Interfaces;
using GeneticToolkit.Mutations;
using GeneticToolkit.Policies.Heaven;
using GeneticToolkit.Policies.Incompatibility;
using GeneticToolkit.Policies.Mutation;
using GeneticToolkit.Policies.Resize;
using GeneticToolkit.Policies.Stop;
using GeneticToolkit.Populations;
using GeneticToolkit.Selections;
using GeneticToolkit.Utils;
using GeneticToolkit.Utils.Configuration;
using GeneticToolkit.Utils.FitnessFunctions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GeneticToolkit.UnitTests.Serialization
{
    public class DynamicObjectFactoryTests
    {
        [Test]
        [TestCase("System.Int32", 10, typeof(int))]
        [TestCase("System.Boolean", true, typeof(bool))]
        [TestCase("System.UInt32", 100, typeof(uint))]
        [TestCase("System.Single", 3.5f, typeof(float))]
        [TestCase("System.Double", 10.235, typeof(double))]
        [TestCase("System.String", "lorem ipsum", typeof(string))]
        public void Factory_Instantiates_Primitive_And_String(string typeName, object value, Type type)
        {
            var objectInfo = new DynamicObjectInfo()
            {
                Type = typeName,
                Parameters = new List<DynamicObjectInfo>(),
                Value = value,
                GenericParameters = new List<string>()
            };
            var result = DynamicObjectFactory<object>.Build(objectInfo);
            Assert.AreEqual(value, result);
        }

        [Test]
        public void Factory_Instantiates_Simple_Class_Type()
        {
            var objectInfo = new DynamicObjectInfo()
            {
                Type = typeof(Tournament).FullName,
                Properties = new List<DynamicObjectInfo>
                {
                    new DynamicObjectInfo
                    {
                        Name = nameof(Tournament.PopulationPercentage),
                        Type = "System.Single",
                        Value = 0.69f
                    }
                },
                GenericParameters = new List<string>()
            };
            var result = DynamicObjectFactory<Tournament>.Build(objectInfo);
            Assert.NotNull(result);
            Assert.AreEqual(0.69f, result.PopulationPercentage);
        }

        [Test]
        public void Factory_Instantiates_Generic_Class_Type()
        {
            var typeName = typeof(Range<>).FullName; 
            var objectInfo = new DynamicObjectInfo
            {
                Type = typeName.Remove(typeName.IndexOf('`')),
                Properties = new List<DynamicObjectInfo>
                {
                    new()
                    {
                        Name = nameof(Range<int>.High),
                        Type = "System.Int32",
                        Value = 10
                    },
                    new()
                    {
                        Name = nameof(Range<int>.Low),
                        Type = "System.Int32",
                        Value = 3
                    }
                },
                GenericParameters = new List<string> {"System.Int32"}
            };
            // Normally instead of specific type, one should use common interface
            var result = DynamicObjectFactory<Range<int>>.Build(objectInfo);
            Assert.NotNull(result);
            Assert.AreEqual(3, result.Low);
            Assert.AreEqual(10, result.High);
        }

        [Test]
        public void Factory_Instantiates_Generic_Class_Type_With_Constructor_Parameters()
        {
            var typeName = typeof(Range<>).FullName; 
            var objectInfo = new DynamicObjectInfo
            {
                Type = typeName.Remove(typeName.IndexOf('`')),
                Parameters = new List<DynamicObjectInfo>
                {
                    new()
                    {
                        Name = nameof(Range<int>.High),
                        Type = "System.Int32",
                        Value = 10
                    },
                    new()
                    {
                        Name = nameof(Range<int>.Low),
                        Type = "System.Int32",
                        Value = 3
                    }
                },
                GenericParameters = new List<string> {"System.Int32"}
            };
            // Normally instead of specific type, one should use common interface
            var result = DynamicObjectFactory<Range<int>>.Build(objectInfo);
            Assert.NotNull(result);
            Assert.AreEqual(3, result.Low);
            Assert.AreEqual(10, result.High);
        }

        [Test]
        public void Factory_Instantiates_Nested_Objects()
        {
            var objectInfo = new DynamicObjectInfo
            {
                Type = typeof(Population).FullName,
                Properties = new List<DynamicObjectInfo>
                {
                    new()
                    {
                        Type = "GeneticToolkit.Mutations.BitwiseFlip",
                        Name = "Mutation"
                    }
                },
                Parameters = new List<DynamicObjectInfo>
                {
                    new()
                    {
                        Name = "Size",
                        Type = "System.Int32",
                        Value = 10
                    }
                },
                GenericParameters = new List<string> {}
            };

            // Normally instead of specific type, one should use common interface
            var result = DynamicObjectFactory<IPopulation>.Build(objectInfo);
            Assert.NotNull(result);
            Assert.NotNull(result.Mutation);
            Assert.AreEqual(typeof(BitwiseFlip), result.Mutation.GetType());
        }

        [Test]
        public void Factory_Serializes_Complex_Instance()
        {
            var fitnessFunction = new FitnessFunction(phenotype => 1);
            var compareCriteria = new SimpleComparison(fitnessFunction, EOptimizationMode.Minimize);
            var geneticAlgorithm = new GeneticAlgorithm
            {
                StopConditions = new IStopCondition[]
                {
                    new TimeSpanCondition(TimeSpan.FromSeconds(25f)),
                    new PopulationDegradation(0.9f),
                    new SufficientIndividual(fitnessFunction, 0.0001f)
                },
                StopConditionMode = EStopConditionMode.Any,
                Population = new Population(fitnessFunction, 30)
                {
                    CompareCriteria = compareCriteria,
                    Crossover = new SinglePointCrossover(),
                    HeavenPolicy = new OneGod(),
                    Mutation = new ArithmeticMutation(new[]
                        {
                            new Range<float>(-10, 10),
                            new Range<float>(-10, 10),
                            new Range<float>(-10, 10),
                            new Range<float>(-10, 10),
                            new Range<float>(-10, 10)
                        },
                        new[]
                        {
                            ArithmeticMutation.EMode.Byte,
                            ArithmeticMutation.EMode.Byte,
                            ArithmeticMutation.EMode.Byte,
                            ArithmeticMutation.EMode.Byte,
                            ArithmeticMutation.EMode.Byte
                        }
                    ),
                    MutationPolicy = new HesserMannerMutation(1, 1, 0.1f),
                    ResizePolicy = new ConstantResizePolicy(),
                    IncompatibilityPolicy = new AllowAll(),
                    SelectionMethod = new Tournament(compareCriteria, 0.01f),
                    StatisticUtilities = new Dictionary<string, IStatisticUtility>()
                }
            };


            var info = DynamicObjectFactory<GeneticAlgorithm>.Serialize(geneticAlgorithm, "GeneticAlgorithm");

            Console.WriteLine(JsonConvert.SerializeObject(info));
            Console.WriteLine(".");
        }

        [Test]
        public void Factory_Serialization_Is_Reversible()
        {
            var fitnessFunction = new FitnessFunction(phenotype => 1);
            var compareCriteria = new SimpleComparison(fitnessFunction, EOptimizationMode.Minimize);
            var geneticAlgorithm = new GeneticAlgorithm
            {
                StopConditions = new IStopCondition[]
                {
                    new TimeSpanCondition(TimeSpan.FromSeconds(25f)),
                    new PopulationDegradation(0.9f),
                    new SufficientIndividual(fitnessFunction, 0.0001f)
                },
                StopConditionMode = EStopConditionMode.Any,
                Population = new Population(fitnessFunction, 30)
                {
                    CompareCriteria = compareCriteria,
                    Crossover = new SinglePointCrossover(),
                    HeavenPolicy = new OneGod(),
                    Mutation = new ArithmeticMutation(new[]
                        {
                            new Range<float>(-10, 10),
                            new Range<float>(-10, 10),
                            new Range<float>(-10, 10),
                            new Range<float>(-10, 10),
                            new Range<float>(-10, 10)
                        },
                        new[]
                        {
                            ArithmeticMutation.EMode.Byte,
                            ArithmeticMutation.EMode.Byte,
                            ArithmeticMutation.EMode.Byte,
                            ArithmeticMutation.EMode.Byte,
                            ArithmeticMutation.EMode.Byte
                        }
                    ),
                    MutationPolicy = new HesserMannerMutation(1, 1, 0.1f),
                    ResizePolicy = new ConstantResizePolicy(),
                    IncompatibilityPolicy = new AllowAll(),
                    SelectionMethod = new Tournament(compareCriteria, 0.01f),
                    StatisticUtilities = new Dictionary<string, IStatisticUtility>()
                }
            };
            
            var info = DynamicObjectFactory<GeneticAlgorithm>.Serialize(geneticAlgorithm, "GeneticAlgorithm");
            var stringified = JsonConvert.SerializeObject(info);
            var destringified = JsonConvert.DeserializeObject<DynamicObjectInfo>(stringified);
            var restoredGeneticAlgorithm = DynamicObjectFactory<GeneticAlgorithm>.Build(destringified);
            Assert.NotNull(restoredGeneticAlgorithm);
            var newStringified = JsonConvert.SerializeObject(info);
            
            Assert.AreEqual(stringified, newStringified);
        }
    }
}