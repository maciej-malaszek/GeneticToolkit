using GeneticToolkit.Genotypes.Primitive;
using System;
using System.Collections.Generic;
using GeneticToolkit;
using GeneticToolkit.Comparisons;
using GeneticToolkit.Crossovers;
using GeneticToolkit.Genotypes.Collective;
using GeneticToolkit.Interfaces;
using GeneticToolkit.Mutations;
using GeneticToolkit.Phenotypes.Collective;
using GeneticToolkit.Policies.Heaven;
using GeneticToolkit.Policies.Incompatibility;
using GeneticToolkit.Policies.Mutation;
using GeneticToolkit.Policies.Resize;
using GeneticToolkit.Policies.Stop;
using GeneticToolkit.Populations;
using GeneticToolkit.Selections;
using GeneticToolkit.Utils;
using GeneticToolkit.Utils.Factories;
using GeneticToolkit.Utils.FitnessFunctions;

namespace SandboxApplication
{
    public class EllipticParameters : IGeneticallySerializable
    {
        private static readonly Range<float>[] Range = {
            new Range<float>(0.00f, 5.0f),
        };

        private static float Scale => Range[0].High - Range[0].Low;
        private static float Step => Scale / ushort.MaxValue;

        private readonly float[] _parameters = new float[5];

        public float X1 { get => _parameters[0]; set => _parameters[0] = value; }
        public float Y1 { get => _parameters[1]; set => _parameters[1] = value; }
        public float X2 { get => _parameters[2]; set => _parameters[2] = value; }
        public float Y2 { get => _parameters[3]; set => _parameters[3] = value; }
        public float A { get => _parameters[4]; set => _parameters[4] = value; }

        public byte[] Serialize()
        {
            var bytes = new List<byte>();
            for (var i = 0; i < _parameters.Length; i++)
            {
                if (i == 4)
                {
                    uint rawValue = (uint)((_parameters[i] - Range[0].Low) / Step);
                    bytes.AddRange(BitConverter.GetBytes(rawValue));
                }
                else
                    bytes.AddRange(BitConverter.GetBytes(_parameters[i]));
            }

            return bytes.ToArray();
        }

        public IGeneticallySerializable Deserialize(byte[] data)
        {
            for (var i = 0; i < _parameters.Length; i++)
            {
                if (i == 4)
                {
                    uint rawValue = BitConverter.ToUInt32(data, sizeof(float) * i);
                    _parameters[i] = rawValue * Step + Range[0].Low;
                }
                else
                    _parameters[i] = BitConverter.ToSingle(data, sizeof(float) * i);
            }

            return this;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            CollectiveGenotype<EllipticParameters> genotypeA = new CollectiveGenotype<EllipticParameters>();
            genotypeA.Genes = new byte[]
            {
                243,
                97,
                132,
                61,
                117,
                55,
                57,
                192,
                17,
                187,
                12,
                63,
                90,
                246,
                55,
                192,
                8,
                58,
                0,
                0
            };


            CollectiveGenotype<EllipticParameters> genotypeB = new CollectiveGenotype<EllipticParameters>();
            genotypeB.Genes = new byte[]
            {
                39,
                0,
                91,
                190,
                177,
                76,
                63,
                192,
                18,
                167,
                179,
                189,
                212,
                128,
                24,
                192,
                212,
                81,
                0,
                0
            };

            IGenotype[] parents = new[] {genotypeA, genotypeB};

            var result = new ArithmeticCrossover(ArithmeticCrossover.EMode.Single).Cross(parents);
            var fitnessFunction = new FitnessFunction((x)=>new Random().NextDouble());
            var factory = new CollectivePhenotypeFactory<EllipticParameters>();
            var individualFactory = new IndividualFactory<CollectiveGenotype<EllipticParameters>, CollectivePhenotype<EllipticParameters>>(factory, fitnessFunction);
            var compareCriteria = new SimpleComparison(fitnessFunction, EOptimizationMode.Minimize);

            var geneticAlgorithm = new GeneticAlgorithm
            {
                StopConditions = new IStopCondition[]
                {
                    new GenerationsLimit(200),
                    new PopulationDegradation(0.9f),
                },
                StopConditionMode = EStopConditionMode.Any,
                Population = new Population(fitnessFunction, 2)
                {
                    CompareCriteria = compareCriteria,
                    Crossover = new ArithmeticCrossover(ArithmeticCrossover.EMode.Single),
                    HeavenPolicy = new OneGod(),
                    Mutation = new BitwiseFlip(),
                    MutationPolicy = new SimpleMutation(0.00f, 0.0f),
                    ResizePolicy = new ConstantResizePolicy(),
                    IndividualFactory = individualFactory,
                    IncompatibilityPolicy = new AllowAll(),
                    SelectionMethod = new RankRoulette(compareCriteria, i => i),
                    StatisticUtilities = new Dictionary<string, IStatisticUtility>()
                }
            };


            geneticAlgorithm.Reset();
            geneticAlgorithm.Population.Initialize(() =>
            {
                var individuals = new IIndividual[geneticAlgorithm.Population.Size];
                for (int i = 0; i < geneticAlgorithm.Population.Size; i++)
                    individuals[i] = geneticAlgorithm.Population.IndividualFactory.CreateFromGenotype(parents[i]);
                return individuals;
            });


            geneticAlgorithm.CreatedNextGeneration += (e, sender) => { Console.WriteLine(sender.Generation); };

            geneticAlgorithm.Run();

            Console.WriteLine("Hello World!");
        }
    }
}
