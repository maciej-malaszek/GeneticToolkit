using System.Collections;
using System.Collections.Generic;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Utils.Statistics
{
    public class FitnessHistory : IStatisticUtility, IList<double>
    {
        protected IList<double> History { get; set; } = new List<double>();

        public int IndexOf(double item)
        {
            return History.IndexOf(item);
        }

        public void Insert(int index, double item)
        {
            History.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            History.RemoveAt(index);
        }

        public double this[int indexer] { get => History[indexer]; set => History[indexer] = value; }

        public void UpdateData(IPopulation population)
        {
            double functionValue = population.CompareCriteria.FitnessFunction.GetValue(population.GetBest());
            History.Add(functionValue);
        }

        public void Reset()
        {
            History.Clear();
        }

        public IEnumerator<double> GetEnumerator()
        {
            return History.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return History.GetEnumerator();
        }

        public void Add(double item)
        {
            History.Add(item);
        }

        public void Clear()
        {
            History.Clear();
        }

        public bool Contains(double item)
        {
            return History.Contains(item);
        }

        public void CopyTo(double[] array, int arrayIndex)
        {
            History.CopyTo(array,arrayIndex);
        }

        public bool Remove(double item)
        {
            return History.Remove(item);
        }

        public int Count { get => History.Count; }
        public bool IsReadOnly { get =>History.IsReadOnly; }
    }
}
