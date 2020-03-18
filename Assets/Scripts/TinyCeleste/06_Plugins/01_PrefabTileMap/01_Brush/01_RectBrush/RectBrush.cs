using TinyCeleste._04_Extension._02_Unity;
using UnityEditor;
using UnityEngine;

namespace TinyCeleste._06_Plugins._01_PrefabTileMap._01_Brush._01_RectBrush
{
    public class RectBrush : Brush
    {
        /// <summary>
        /// 鼠标按下时的坐标
        /// </summary>
        protected Vector2Int cornerBegin;

        /// <summary>
        /// 鼠标释放时的坐标
        /// </summary>
        protected Vector2Int cornerEnd;

        /// <summary>
        /// 右键取消事件
        /// </summary>
        protected bool cancel;

        public RectBrush()
        {
            icon = "Grid.BoxTool";
        }

        /// <summary>
        /// 持续触发
        /// </summary>
        public override void Update()
        {
            // 鼠标按下时不再绘制提示外框
            if (!window.isMouseDowning)
                DrawOutWire();
            TriggerMouseCellPosChange();
        }

        /// <summary>
        /// 开始一个矩形操作
        /// </summary>
        public override void OnMouseDown()
        {
            cornerBegin = map.mouseCellPos;
        }

        /// <summary>
        /// 鼠标抬起，若无取消，则执行矩形操作
        /// </summary>
        public override void OnMouseUp()
        {
            if (cancel)
            {
                cancel = false;
                HideOrShowLastCoverTiles(false);
                return;
            }

            var cellPositions = EX_Vector2Int.Range(cornerBegin, cornerEnd);
            for (int y = 0; y < cellPositions.Length; y++)
            {
                for (int x = 0; x < cellPositions[y].Length; x++)
                {
                    RectOperation(cellPositions[y][x]);
                }
            }
        }

        /// <summary>
        /// 逐单元的矩形操作
        /// </summary>
        /// <param name="cellPos"></param>
        protected virtual void RectOperation(Vector2Int cellPos)
        {
        }

        /// <summary>
        /// 右键中断矩形操作
        /// </summary>
        public override void OnRightMouseUp()
        {
            cancel = true;
        }

        /// <summary>
        /// 绘制范围网格
        /// </summary>
        public override void OnMouseDowning()
        {
            cornerEnd = map.mouseCellPos;
            var leftBottom = new Vector2Int();
            var rightTop = new Vector2Int();
            EX_Vector2Int.TwoCorner(ref leftBottom, ref rightTop, cornerBegin, cornerEnd);
            var _leftBottom = map.GridToWorld(leftBottom) - map.cellSize / 2;
            var _rightTop = map.GridToWorld(rightTop) + map.cellSize / 2;
            var _center = (_leftBottom + _rightTop) / 2;
            var _size = _rightTop - _leftBottom;
            Handles.color = wireframeColor;
            Handles.DrawWireCube(_center, _size);
        }

        /// <summary>
        /// 上一帧覆盖范围内的Tile
        /// </summary>
        private E_PrefabTile[][] lastCoverTiles;

        /// <summary>
        /// 鼠标网格位置发生变化时触发
        /// 触发频率比OnMouseDowning更低
        /// 对性能敏感的操作可放置于此
        /// </summary>
        public override void OnMouseCellPosChange()
        {
            OnMouseDowning();
            // 释放上一帧隐藏的Tile
            HideOrShowLastCoverTiles(false);

            // 暂时隐藏当前Rect覆盖位置地图中的的Tile
            var cellPositions = EX_Vector2Int.Range(cornerBegin, cornerEnd);
            lastCoverTiles = new E_PrefabTile[cellPositions.Length][];
            for (int y = 0; y < cellPositions.Length; y++)
            {
                lastCoverTiles[y] = new E_PrefabTile[cellPositions[y].Length];
                for (int x = 0; x < cellPositions[y].Length; x++)
                {
                    lastCoverTiles[y][x] = map.GetTileByCellPos(cellPositions[y][x]);
                }
            }

            HideOrShowLastCoverTiles(true);
        }

        protected void HideOrShowLastCoverTiles(bool hide)
        {
            if (lastCoverTiles == null) return;
            for (int y = 0; y < lastCoverTiles.Length; y++)
            {
                for (int x = 0; x < lastCoverTiles[y].Length; x++)
                {
                    if (lastCoverTiles[y][x] != null)
                    {
                        if (hide)
                        {
                            lastCoverTiles[y][x].TempHide();
                        }
                        else
                        {
                            lastCoverTiles[y][x].TempShow();
                        }
                    }
                }
            }
        }
    }
}