using System.Collections.Generic;

namespace TinyCeleste._04_Extension._03_CSharp
{
    public static class EX_List
    {
        // 判断两个列表的元素是否有重合
        public static bool Match<T>(List<T> list1, List<T> list2)
        {
            foreach (var item1 in list1)
            {
                foreach (var item2 in list2)
                {
                    if (item1.Equals(item2)) return true;
                }
            }

            return false;
        }
    }
}