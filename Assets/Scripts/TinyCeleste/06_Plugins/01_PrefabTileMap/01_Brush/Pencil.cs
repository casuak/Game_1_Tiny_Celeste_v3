using UnityEngine;

namespace TinyCeleste._06_Plugins._01_PrefabTileMap._01_Brush
{
    public class Pencil : Brush
    {
        public Pencil()
        {
            icon = "Grid.PaintTool";
        }

        /// <summary>
        /// 重新生成笔头
        /// </summary>
        private void RegeneratePencilHead()
        {
//            window.tempTileArray2D = new PrefabTileArray2D(new Vector2Int(0, 0), new Vector2Int(0, 0));
        }

        /// <summary>
        /// 销毁当前笔头
        /// </summary>
        private void DestroyPencilHead()
        {
            
            window.tempTileArray2D = null;
            window.tempTileArray2DOffset = Vector2Int.zero;
        }
    }
}