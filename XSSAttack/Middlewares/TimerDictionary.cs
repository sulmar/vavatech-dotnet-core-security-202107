using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace XSSAttack.Middlewares
{
    public class TimerDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> dictionary;
        private readonly Dictionary<TKey, Timer> timers;
        private readonly int duration;

        public TimerDictionary(int duration)
        {
            dictionary = new Dictionary<TKey, TValue>();
            timers = new Dictionary<TKey, Timer>();

            this.duration = duration;
        }

        public TValue this[TKey key]
        {
            get => dictionary[key];
            set => dictionary[key] = value;
        }

        public ICollection<TKey> Keys => dictionary.Keys;

        public ICollection<TValue> Values => dictionary.Values;

        public int Count => dictionary.Count;

        public bool IsReadOnly => false;

        public void Add(TKey key, TValue value)
        {
            dictionary.Add(key, value);

            Timer timer = new Timer(new TimerCallback(OnTimer), key, duration, duration);

            timers.Add(key, timer);
        }

        private void OnTimer(object state)
        {
            TKey key = (TKey)state;

            timers.Remove(key);

            dictionary.Remove(key);

            Trace.WriteLine($"Removed timer {key}");

            // timers[key].Dispose();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            dictionary.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(TKey key)
        {
            return dictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        public bool Remove(TKey key)
        {
            return dictionary.Remove(key);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            return dictionary.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator() => dictionary.GetEnumerator();
    }
}
