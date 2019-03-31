using GeneticToolkit.Utils.Data;

namespace GeneticToolkit.Interfaces
{
    public interface IGeneticSerializable
    {
        GeneticAlgorithmParameter Serialize();
    }
}
