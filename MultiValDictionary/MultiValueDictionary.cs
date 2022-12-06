using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiValDictionary
{

    public class MultiValueDictionary<K, T> : IMultiValueDictionary<K, T>
    {
        private int[] _buckets;
        private Entry<K, T>[] _entries;

        private int _freeEntryIndex;

        public MultiValueDictionary(int capasity)
        {
            _entries = new Entry<K, T>[capasity];
            _buckets = new int[capasity];
            InitializeBuckets(_buckets);
        }

        public MultiValueDictionary() : this(10)
        { }


        public bool Add(K key, T value)
        {
            int targetBucketIndex = GetTargetBucketIndex(key);

            if (BucketIndexIsClear(targetBucketIndex))
            {
                _buckets[targetBucketIndex] = _freeEntryIndex;
                _entries[_freeEntryIndex] = CreateEntry(key, value);
                UpdateFreeEntryIndex();

                return true;
            }
            else
            {
                var indexForNewEntry = GetEntryIndexByBucket(targetBucketIndex);
                var lastAddedEntry = GetEntryByIndex(indexForNewEntry);

                if (!IsContainsValue(indexForNewEntry, value))
                {
                    _entries[_freeEntryIndex] = lastAddedEntry;
                    _entries[indexForNewEntry] = CreateEntry(key, value, _freeEntryIndex);
                    UpdateFreeEntryIndex();

                    return true;
                }
                else
                    return false;              
            }
        }
        public bool RemoveKey(K key)
        {
            int targetBucketIndex = GetTargetBucketIndex(key);

            if (BucketIndexIsClear(targetBucketIndex))
                return false;

            int entryIndex = GetEntryIndexByBucket(targetBucketIndex);
            RemoveEntries(entryIndex);

            if (entryIndex < _freeEntryIndex)
                _freeEntryIndex = entryIndex;

            _buckets[targetBucketIndex] = -1;

            return true;
        }

       
        public bool RemoveValue(K key, T value)
        {
            int targetBucketIndex = GetTargetBucketIndex(key);

            if (BucketIndexIsClear(targetBucketIndex))
                return false;

            var entryIndex = GetEntryIndexByBucket(targetBucketIndex);
            var lastAddedEntry = GetEntryByIndex(entryIndex);

            if (IsContainsValue(entryIndex, value))
            {
                RemoveValue(lastAddedEntry, value);
                return true;
            }

            return false;
        }

        public IEnumerable<T> GetValues(K key)
        {
            int targetBucketIndex = GetTargetBucketIndex(key);

            if (BucketIndexIsClear(targetBucketIndex))
                return Enumerable.Empty<T>();

            var entryIndex = GetEntryIndexByBucket(targetBucketIndex);

            var result = new List<T>();
            result.AddRange(GetChildValues(entryIndex));
            return result;

        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private void RemoveEntries(int entryIndex)
        {
            RemoveChildEntriesCascade(entryIndex);
            _entries[entryIndex] = null;
        }

        private void InitializeBuckets(int[] buckets)
        {
            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i] = -1;
            }
        }

        private IEnumerable<T> GetChildValues(int index)
        {
            var rootEntry = _entries[index];

            if (rootEntry != null)
            {
                var result = new List<T>() { rootEntry.Value };
                if (rootEntry.NextIndex != -1)
                {
                    result.AddRange(GetChildValues(rootEntry.NextIndex));
                }

                return result;
            }
            else
                return Enumerable.Empty<T>();
        }

        
        private void RemoveChildEntriesCascade(int index)
        {
            var entry = _entries[index];

            if (entry == null)
                return;

            if (entry.NextIndex != -1)
                RemoveEntries(entry.NextIndex);

        }

        private void RemoveValue(Entry<K, T> rootEntry, T value)
        {

            if (rootEntry != null && rootEntry.NextIndex != -1)
            {
                var nextEntry = _entries[rootEntry.NextIndex];

                if (nextEntry != null && nextEntry.Value.Equals(value))
                {
                    _entries[rootEntry.NextIndex] = null;
                    rootEntry.NextIndex = nextEntry.NextIndex;
                }

            }

        }

        private bool IsContainsValue(int index, T value)
        {
            var rootEntry = _entries[index];
            if (rootEntry != null && rootEntry.Value.Equals(value))
                return true;

            if (rootEntry.NextIndex == -1)
                return false;

            return IsContainsValue(rootEntry.NextIndex, value);
        }

        private void UpdateFreeEntryIndex()
        {
            for (int i = _freeEntryIndex + 1; i < _entries.Length; i++)
            {
                if (_entries[i] == null)
                {
                    _freeEntryIndex = i;
                    return;
                }
            }
            Resize();
        }
        private void Resize()
        {
            ResizeBuckets();
            ResizeEntries();
        }

        private void ResizeBuckets()
        {
            var newBuckets = new int[_buckets.Length * 2];
            InitializeBuckets(newBuckets);
            _buckets.CopyTo(newBuckets, 0);
            _buckets = newBuckets;
        }

        private void ResizeEntries()
        {
            var newEntries = new Entry<K, T>[_entries.Length * 2];
            _entries.CopyTo(newEntries, 0);
            _entries = newEntries;
        }

        private int GetEntryIndexByBucket(int backetIndex)
        {
            return _buckets[backetIndex];
        }

        private Entry<K, T> GetEntryByIndex(int entryIndex)
        {
            return _entries[entryIndex];
        }

        private Entry<K, T> CreateEntry(K key, T value, int nextIndex = -1)
        {
            return new Entry<K, T>()
            {
                Key = key,
                Value = value,
                NextIndex = nextIndex,
            };
        }

        private bool BucketIndexIsClear(int targetBucket)
        {
            return _buckets[targetBucket] == -1;
        }
        private int GetTargetBucketIndex(K key)
        {
            if (key == null) throw new ArgumentNullException("key");

            int hashCode = key.GetHashCode();
            int targetBucket = Math.Abs(hashCode % _buckets.Length);
            return targetBucket;
        }



    }
}
