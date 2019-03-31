using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace GeneticToolkit.Utils.Data
{
    public class GeneticAlgorithmSettings
    {
        public GeneticAlgorithmParameter SelectionMethod { get; set; }
        public GeneticAlgorithmParameter Crossover { get; set; }
        public GeneticAlgorithmParameter HeavenPolicy { get; set; }
        public GeneticAlgorithmParameter IncompatibilityPolicy { get; set; }
        public GeneticAlgorithmParameter ResizePolicy { get; set; }
        public GeneticAlgorithmParameter IndividualFactory { get; set; }
        public string Type { get; set; }
        public string[] GenericArguments { get; set; }
        public IDictionary<string,object> CustomParameters { get; set; }

        public GeneticAlgorithmSettings()
        {
        }

        public string ToJson(bool minimal = false)
        {
            return JsonConvert.SerializeObject(this, minimal ? Formatting.None : Formatting.Indented);
        }

        public void Export(string destinationPath, bool minimal = false)
        {
            File.WriteAllText(destinationPath.ToString(), ToJson(minimal));
        }

        public static GeneticAlgorithmSettings Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<GeneticAlgorithmSettings>(json);
        }
    }
}
