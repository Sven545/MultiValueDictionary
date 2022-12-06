namespace MultiValDictionary
{
    class Program
    {
        static string List2String<T>(IEnumerable<T> list)
        {
            return string.Join(" ", list.Select(item => item.ToString()).ToArray());
        }
        static void Main(string[] args)
        {
            MultiValueDictionary<string, int> mvDictionary = new MultiValueDictionary<string, int>();
            mvDictionary.Add("hello", 1);
            mvDictionary.Add("hello", 13);
            mvDictionary.Add("goodbye", 8);
            bool result = mvDictionary.Add("hello", 5);
            Console.WriteLine(result); // true
            result = mvDictionary.Add("hello", 1);
            Console.WriteLine(result); // false
            result = mvDictionary.RemoveKey("aloha");
            Console.WriteLine(result); // false
            result = mvDictionary.RemoveKey("goodbye");
            Console.WriteLine(result); // true
            result = mvDictionary.RemoveValue("goodbye", 13);
            Console.WriteLine(result); // false
            result = mvDictionary.RemoveValue("hello", 13);
            Console.WriteLine(result); // true
            IEnumerable<int> list = mvDictionary.GetValues("aloha");
            Console.WriteLine(List2String(list)); // empty list
            list = mvDictionary.GetValues("hello");
            Console.WriteLine(List2String(list)); // 1 5
        }
    }

}