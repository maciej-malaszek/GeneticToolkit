using JetBrains.Annotations;

namespace GeneticToolkit.Interfaces
{
    [PublicAPI]
    public interface ICrossover
    {
        int ParentsCount { get; }

        int ChildrenCount { get; }

        /// <summary>
        /// <para>While performing cut on gene array, makes sure that cut point will be multiplicity of this value.</para>
        /// <para>Useful when cross may produce incompatible number from two compatible.</para>
        /// <para>e.g. BitAlign = 8 will align entire bytes and value 32 will align integers.</para>
        /// </summary>
        int BitAlign { get; set; }

        IGenotype[] Cross(IGenotype[] parents);
    }
}
