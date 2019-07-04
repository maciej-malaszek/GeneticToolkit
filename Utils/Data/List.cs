using System;

namespace GeneticToolkit.Utils.Data
{
    public class List<T>
    {
        private T[] _items = new T[1];
        public int Count { get; private set; }

        public void Add(T item)
        {
            if(Count + 1 == _items.Length)
                Resize();
            _items[Count++] = item;
        }

        public T this[int indexer]
        {
            get => _items[indexer];
            set => _items[indexer] = value;
        } 
        private void Resize()
        {
            var items = new T[_items.Length * 2];
            _items.CopyTo(items,0);
            _items = items;
        }

        public int IndexOf(T value)
        {
            return Array.IndexOf(_items, value);
        }

        public List()
        {
        }

        public List(T[] array)
        {
            _items = array;
            Count = _items.Length;
        }

        public T[] ToArray()
        {
            var array = new T[Count];
            for (int i = 0; i < Count; i++)
                array[i] = _items[i];
            return array;
        }
        public void Clear()
        {
            _items = new T[1];
            Count = 0;
        }
    }

    public static class ListExtensions
    {
        public static int Sum(this List<int> list, Func<int,int> selector)
        {
            int sum = 0;
            for (int i = 0; i < list.Count; i++)
                sum += selector(list[i]);
            return sum;
        }

        public static double Sum(this List<double> list, Func<double,double> selector)
        {
            double sum = 0;
            for (int i = 0; i < list.Count; i++)
                sum += selector(list[i]);
            return sum;
        }
    }
}
