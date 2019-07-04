namespace GeneticToolkit.Interfaces
{
    public interface IGeneticallySerializable
    {
        byte[] Serialize();
        IGeneticallySerializable Deserialize(byte[] data);
    }
}