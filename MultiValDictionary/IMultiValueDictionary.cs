namespace MultiValDictionary
{

    interface IMultiValueDictionary<K, T> : IEnumerable<T>
    {
        // false, if value exist for this key
        bool Add(K key, T value);
        // false, if key doesn't exist
        bool RemoveKey(K key);
        // false, if value doesn't exist for this key
        bool RemoveValue(K key, T value);
        // just return all values
        IEnumerable<T> GetValues(K key);
    }


}
