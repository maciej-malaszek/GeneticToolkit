using System;
using System.Collections.Generic;
using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils.Configuration;
using GeneticToolkit.Utils.Data;
using Newtonsoft.Json.Linq;

namespace GeneticToolkit.Utils.Factories
{
    public class ScaledPhenotypeFactory<TPhenotype, TOutput> : IPhenotypeFactory<TPhenotype>
        where TPhenotype : IScaledPhenotype<TOutput>, new()
    {
        public Range<TOutput> Range { get; set; }

        public ScaledPhenotypeFactory(IDictionary<string,object> parameters)
        {
            var rangeJObject = (JObject) parameters["Range"];
            var rangeParameter = rangeJObject.ToObject<GeneticAlgorithmParameter>();
            Type type = Importer.GetTypeFrom(rangeParameter, typeof(Range<TOutput>));
            Range = Activator.CreateInstance(type, args: rangeParameter.Params) as Range<TOutput>;
        }

        public ScaledPhenotypeFactory(Range<TOutput> range)
        {
            Range = range;
        }
        public TPhenotype Make(IGenotype genotype)
        {
            return new TPhenotype()
            {
                Genotype = genotype,
                Scale = Range
            };
        }

        public GeneticAlgorithmParameter Serialize()
        {
            return new GeneticAlgorithmParameter(this)
            {
                Params = new Dictionary<string, object>()
                {
                    {"Range", Range.Serialize()}
                }
            };
        }
    }
}
