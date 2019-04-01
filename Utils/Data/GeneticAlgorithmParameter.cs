using System.Collections.Generic;
using System.Linq;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Utils.Data
{
    public class GeneticAlgorithmParameter
    {
        // Required by JsonConvert.Deserialize
        public GeneticAlgorithmParameter()
        {
        }

        public GeneticAlgorithmParameter(IGeneticSerializable serializableObject)
        {
            var type = serializableObject.GetType();
            Type = type.IsGenericType ? type.GetGenericTypeDefinition().FullName : type.FullName;
            GenericArguments = type.GenericTypeArguments.Select(x => x.FullName).ToArray();
        }

        public string Type { get; set; }

        public string[] GenericArguments { get; set; }

        public IDictionary<string, object> Params { get; set; } = new Dictionary<string, object>();
    }
}