using TinyCeleste._06_Plugins._01_PrefabTileMap._01_Brush._01_RectBrush;
using UnityEngine;

namespace TinyCeleste._06_Plugins._01_PrefabTileMap._01_Brush
{
    public class Pencil : Brush
    {
        /// <summary>
        /// 笔头
        /// </summary>
        private E_PrefabTile pencilHead;

        /// <summary>
        /// 上一次笔头隐藏的Tile
        /// </summary>
        private E_PrefabTile lastCoverTile;

        public Pencil()
        {
            icon = "Grid.PaintTool";
        }

        public override void Update()
        {
            base.Update();
            // 更新笔头位置
            if (pencilHead != null)
                pencilHead.SetCellPos(map.mouseCellPos);
            // 隐藏笔头位置的地图元素
            if (!window.isMouseDowning)
            {
                if (lastCoverTile != null)
                    lastCoverTile.TempShow();
                lastCoverTile = map.GetTileByCellPos(map.mouseCellPos);
                if (lastCoverTile != null)
                    lastCoverTile.TempHide();
            }
        }

        public override void OnEnter()
        {
            ReGeneratePencilHead();
        }

        public override void OnExit()
        {
            DestroyPencilHead();
        }

        /// <summary>
        /// 重新生成笔头
        /// </summary>
        public override void OnTileChanged()
        {
            ReGeneratePencilHead();
        }

        /// <summary>
        /// 重新生成笔头
        /// </summary>
        private void ReGeneratePencilHead()
        {
            DestroyPencilHead();
            pencilHead = E_PrefabTile.Create(window.currentTile)?.SetMap(map).SetCellPos(map.mouseCellPos);
        }

        /// <summary>
        /// 摧毁笔头
        /// </summary>
        private void DestroyPencilHead()
        {
            if (pencilHead != null)
                pencilHead.DestroySelfImmediate();
        }

        /// <summary>
        /// 绘制到地图上
        /// </summary>
        private void DrawToMap()
        {
            map.CreateTileAt(map.mouseCellPos, window.currentTile);
        }

        public override void OnMouseDown()
        {
            DrawToMap();
        }

        public override void OnMouseCellPosChange()
        {
            DrawToMap();
        }
    }
}