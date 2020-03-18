using UnityEngine;

namespace TinyCeleste._05_MyTool._06_Math
{
    public static class Tool_Grid
    {
        /// <summary>
        /// 规范化坐标，将坐标转化为最近的方块中心的坐标
        /// 构造子空间
        /// 子空间的原点坐标 center
        /// 子空间的坐标轴在世界空间的表示分别为
        /// (gridSize.x, 0, 0)
        /// (0, gridSize,y, 0)
        /// (0, 0, 1)
        /// </summary>
        /// <param name="pos0">需要进行转化的坐标</param>
        /// <param name="center">方格系统的中心坐标</param>
        /// <param name="gridSize">方格大小</param>
        /// <param name="gridGap">方格间隙</param>
        /// <returns></returns>
        public static Vector2 WorldToGridCenter(Vector2 pos0, Vector2 center, Vector2 gridSize, Vector2 gridGap)
        {
            gridSize += gridGap;
            // 偏移量归入一个格子大小的正负值
            center = new Vector2(center.x % gridSize.x, center.y % gridSize.y);
            // 有gap时需要修正偏移量
            center -= gridGap * 0.5f;

            Vector4 c0 = new Vector4(gridSize.x, 0, 0, 0);
            Vector4 c1 = new Vector4(0, gridSize.y, 0, 0);
            Vector4 c2 = new Vector4(0, 0, 1, 0);
            Vector4 c3 = new Vector4(center.x, center.y, 0, 1);
            // 子空间到世界空间的变换矩阵
            Matrix4x4 cToW = new Matrix4x4(c0, c1, c2, c3);
            // 世界空间到子空间的变换矩阵
            Matrix4x4 wToC = cToW.inverse;

            // 先将坐标变换到子空间
            Vector3 pos1 = wToC.MultiplyPoint(pos0);

            // 转移到最近的中心坐标
            pos1 = new Vector2((int) pos1.x + 0.5f * IsPositive(pos1.x), (int) pos1.y + 0.5f * IsPositive(pos1.y));

            // 转移回世界空间
            return cToW.MultiplyPoint(pos1);
        }

        /// <summary>
        /// 返回世界坐标在方格坐标中的坐标
        /// </summary>
        /// <param name="pos0">世界坐标</param>
        /// <param name="cellCenter"></param>
        /// <param name="cellSize"></param>
        /// <param name="cellGap"></param>
        /// <returns></returns>
        public static Vector2Int WorldToGrid(Vector2 pos0, Vector2 cellCenter, Vector2 cellSize, Vector2 cellGap)
        {
            cellSize += cellGap;
            // XXX 偏移量归入一个格子大小的正负值
//            cellCenter = new Vector2(cellCenter.x % cellSize.x, cellCenter.y % cellSize.y);
            // 有gap时需要修正偏移量
            cellCenter -= cellGap * 0.5f;

            Vector4 c0 = new Vector4(cellSize.x, 0, 0, 0);
            Vector4 c1 = new Vector4(0, cellSize.y, 0, 0);
            Vector4 c2 = new Vector4(0, 0, 1, 0);
            Vector4 c3 = new Vector4(cellCenter.x, cellCenter.y, 0, 1);
            // 子空间到世界空间的变换矩阵
            Matrix4x4 cToW = new Matrix4x4(c0, c1, c2, c3);
            // 世界空间到子空间的变换矩阵
            Matrix4x4 wToC = cToW.inverse;

            // 将坐标变换到子空间
            Vector3 pos1 = wToC.MultiplyPoint(pos0);

            // 转移到最近的中心坐标
            pos1 = new Vector2((int) pos1.x + 0.5f * IsPositive(pos1.x), (int) pos1.y + 0.5f * IsPositive(pos1.y));
            
            return new Vector2Int(Mathf.RoundToInt(pos1.x - 0.5f), Mathf.RoundToInt(pos1.y - 0.5f));
        }

        /// <summary>
        /// 返回方格坐标的世界坐标
        /// </summary>
        /// <param name="gridPos">方格坐标</param>
        /// <param name="gridCenter"></param>
        /// <param name="gridSize"></param>
        /// <param name="gridGap"></param>
        /// <returns></returns>
        public static Vector2 GridToWorld(Vector2Int gridPos, Vector2 gridCenter, Vector2 gridSize, Vector2 gridGap)
        {
            Vector2 pos0 = gridPos + Vector2.one * 0.5f;
            gridSize += gridGap;
            // 有gap时需要修正偏移量
            gridCenter -= gridGap * 0.5f;

            Vector4 c0 = new Vector4(gridSize.x, 0, 0, 0);
            Vector4 c1 = new Vector4(0, gridSize.y, 0, 0);
            Vector4 c2 = new Vector4(0, 0, 1, 0);
            Vector4 c3 = new Vector4(gridCenter.x, gridCenter.y, 0, 1);
            // 子空间到世界空间的变换矩阵
            Matrix4x4 cToW = new Matrix4x4(c0, c1, c2, c3);

            return cToW.MultiplyPoint(pos0);
        }

        public static int IsPositive(float a)
        {
            return a > 0 ? 1 : -1;
        }

        public static int IsGreater(float a, float b)
        {
            return a > b ? 1 : -1;
        }
    }
}