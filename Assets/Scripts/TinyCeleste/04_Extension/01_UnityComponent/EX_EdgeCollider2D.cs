using UnityEngine;

namespace TinyCeleste._04_Extension._01_UnityComponent
{
    public static class EX_EdgeCollider2D
    {
        /// <summary>
        /// 返回指定索引的点的世界坐标
        /// 索引非法时返回第一个顶点的信息
        /// </summary>
        /// <param name="edgeCollider2D"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Vector2 GetPointPos(this EdgeCollider2D edgeCollider2D, int index)
        {
            if (index < 0 || index >= edgeCollider2D.pointCount) index = 0;
            return -edgeCollider2D.points[index] + edgeCollider2D.transform.GetPos2D();
        }
    }
}