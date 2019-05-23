using System;

namespace GeneticToolkit.Utils.Data
{
    public struct ContemptibleKeyValuePair<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
    }
    public class ContemptibleDictionary<TKey, TValue>
    {
        private TKey[] _keys = new TKey[1];
        private TValue[] _values = new TValue[1];
        private int _lastIndex = 0;
        private int _length = 1;
        public int Count { get; private set; } = 0;

        public TKey[] Keys()
        {
            return _keys;
        }

        private int GetIndex(TKey key)
        {
            if (key == null)
                return -1;
            return Array.IndexOf(_keys, key);
        }

        public bool HasKey(TKey key)
        {
            return Array.IndexOf(_keys, key) >= 0;
        }

        public TValue this[TKey key]
        {
            get => _values[GetIndex(key)];
            set
            {
                int index = GetIndex(key);
                if (index == -1)
                {
                    index = _lastIndex++;
                    Count++;
                }

                if(index >= _length)
                    Resize();

                _keys[index] = key;
                _values[index] = value;
            }
        }

        private void Resize()
        {
            _length += 1;
            var keys = new TKey[_length];
            var values = new TValue[_length];

            _keys.CopyTo(keys,0);
            _values.CopyTo(values, 0);

            _keys = keys;
            _values = values;
        }

        public ContemptibleDictionary()
        {

        }

        public ContemptibleDictionary(ContemptibleKeyValuePair<TKey, TValue>[] data)
        {
            foreach (ContemptibleKeyValuePair<TKey, TValue> t in data)
                this[t.Key] = t.Value;
        }

        public TValue[] GetValues()
        {
            return _values;
        }
    }
}
