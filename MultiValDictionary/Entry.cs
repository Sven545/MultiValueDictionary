namespace MultiValDictionary
{
    public class Entry<K, T>
    {
        public K Key { get; set; }
        public T Value { get; set; }
        public int NextIndex { get; set; } = -1;
    }
}
