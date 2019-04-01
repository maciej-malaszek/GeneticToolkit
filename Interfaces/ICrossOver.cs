using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GeneticToolkit.Interfaces
{
    public interface ICrossover : IGeneticSerializable
    {
        int ParentsCount { get; }

        int ChildrenCount { get; }

        IList<IGenotype> Cross(IList<IGenotype> parents);
    }
}
