using System.Collections.Generic;

namespace TinyCeleste._04_Extension._03_CSharp
{
    public static class EX_Dictionary
    {
        public static bool Put<T1, T2>(this Dictionary<T1, T2> dictionary, T1 key, T2 value)
        {
            if (dictionary.ContainsKey(key)) return false;
            dictionary.Add(key, value);
            return true;
        }

        public static T2 Get<T1, T2>(this Dictionary<T1, T2> dictionary, T1 key)
        {
            dictionary.TryGetValue(key, out var value);
            return value;
        }
    }
}