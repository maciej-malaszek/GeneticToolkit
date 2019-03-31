using System.Collections.Generic;

namespace GeneticToolkit.Interfaces
{
    public interface ICrossover : IGeneticSerializable
    {
        int ParentsCount { get; }

        int ChildrenCount { get; }

        IList<IGenotype> Cross(IList<IGenotype> parents);
    }
}
