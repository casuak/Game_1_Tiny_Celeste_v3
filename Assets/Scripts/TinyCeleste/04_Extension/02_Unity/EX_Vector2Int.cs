using UnityEngine;

namespace TinyCeleste._04_Extension._02_Unity
{
    public static class EX_Vector2Int
    {
        /// <summary>
        /// 返回由两个对角点定义的矩形范围内的所有Vector2Int
        /// 包括两个边界
        /// </summary>
        /// <param name="corner1"></param>
        /// <param name="corner2"></param>
        /// <returns>逐行的二维整数坐标数组</returns>
        public static Vector2Int[][] Range(Vector2Int corner1, Vector2Int corner2)
        {
            var leftBottom = new Vector2Int();
            var rightTop = new Vector2Int();
            TwoCorner(ref leftBottom, ref rightTop, corner1, corner2);
            var size = rightTop - leftBottom + Vector2Int.one;
            var array = new Vector2Int[size.y][];
            for (int y = leftBottom.y; y <= rightTop.y; y++)
            {
                array[y - leftBottom.y] = new Vector2Int[size.x];
                for (int x = leftBottom.x; x <= rightTop.x; x++)
                {
                    array[y - leftBottom.y][x - leftBottom.x] = new Vector2Int(x, y);
                }
            }

            return array;
        }

        /// <summary>
        /// 根据两个角落返回左下和右上坐标
        /// </summary>
        /// <returns></returns>
        public static void TwoCorner(ref Vector2Int leftBottom, ref Vector2Int rightTop, Vector2Int corner1,
            Vector2Int corner2)
        {
            leftBottom.x = corner1.x <= corner2.x ? corner1.x : corner2.x;
            leftBottom.y = corner1.y <= corner2.y ? corner1.y : corner2.y;
            rightTop.x = corner1.x > corner2.x ? corner1.x : corner2.x;
            rightTop.y = corner1.y > corner2.y ? corner1.y : corner2.y;
        }
    }
}