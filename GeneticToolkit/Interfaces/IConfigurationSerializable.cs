using GeneticToolkit.Utils.Data;

namespace GeneticToolkit.Interfaces
{
    public interface IConfigurationSerializable
    {
        GeneticAlgorithmParameter Serialize();
    }
}
