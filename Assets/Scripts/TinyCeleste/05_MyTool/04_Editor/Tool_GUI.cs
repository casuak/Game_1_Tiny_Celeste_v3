using UnityEngine;

namespace TinyCeleste._05_MyTool._04_Editor
{
    public static partial class Tool_GUI
    {
        /// <summary>
        /// 获得归一化的场景坐标
        /// 不考虑Z轴, Cell Layout, Cell Swizzle
        /// </summary>
        /// <param name="originalPos"></param>
        /// <param name="grid"></param>
        /// <param name="center"></param>
        /// <returns></returns>
        public static Vector2 GetNormalizedPosition(Vector2 originalPos, Grid grid, Vector2 center)
        {
            Vector2 gridSize = grid.cellSize + grid.cellGap;
            return Vector2.zero;
        }
    }
}