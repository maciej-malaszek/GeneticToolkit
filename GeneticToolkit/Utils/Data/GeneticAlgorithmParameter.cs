using System;
using System.Collections.Generic;
using System.Linq;
using GeneticToolkit.Interfaces;
using JetBrains.Annotations;

namespace GeneticToolkit.Utils.Data
{
    [PublicAPI]
    public class GeneticAlgorithmParameter
    {
        public GeneticAlgorithmParameter()
        {
        }

        public GeneticAlgorithmParameter(IConfigurationSerializable serializableObject)
        {
            Type type = serializableObject.GetType();
            Type = type.IsGenericType ? type.GetGenericTypeDefinition().FullName : type.FullName;
            GenericArguments = type.GenericTypeArguments.Select(x => x.FullName).ToArray();
        }

        public string Type { get; set; }

        public string[] GenericArguments { get; set; }

        public IDictionary<string, object> Params { get; set; } = new Dictionary<string, object>();
    }
}