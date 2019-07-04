using JetBrains.Annotations;

namespace GeneticToolkit.Utils.DataStructs
{
    [PublicAPI]
    public struct Vector2F64
    {
        public int Identifier { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }
}