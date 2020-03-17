using System;
using Boo.Lang;
using TinyCeleste._01_Framework;
using TinyCeleste._05_MyTool._04_Editor;
using TinyCeleste._05_MyTool._06_Math;
using UnityEngine;

namespace TinyCeleste._06_Plugins._01_PrefabTileMap
{
    /// <summary>
    /// 瓦片地图
    /// </summary>
    public class E_PrefabTileMap : Entity
    {
        /// <summary>
        /// 与原生的Tilemap共享同一grid组件
        /// 需要手动赋值
        /// </summary>
        public Grid grid;

        /// <summary>
        /// 当前网格地图大小
        /// </summary>
        public Vector2 gridSize => tileArray2D.capacity;
        
        /// <summary>
        /// 网格大小
        /// </summary>
        public Vector2 cellSize => grid.cellSize;
        
        /// <summary>
        /// 网格间隔
        /// </summary>
        public Vector2 cellGap => grid.cellGap;

        /// <summary>
        /// 网格中心
        /// </summary>
        public Vector2 cellCenter => grid.transform.position;

        /// <summary>
        /// 标准化的鼠标坐标
        /// </summary>
        public Vector2 normalizedMousePos => WorldToGridCenter(Tool_GUI.GetMouseWorldPosition());

        /// <summary>
        /// 网格中的鼠标坐标
        /// </summary>
        public Vector2Int mouseGridPos => WorldToGrid(Tool_GUI.GetMouseWorldPosition());

        /// <summary>
        /// 瓦片二维数组
        /// </summary>
        [HideInInspector]
        public PrefabTileArray2D tileArray2D;

        /// <summary>
        /// 世界坐标转化为网格坐标
        /// </summary>
        /// <param name="worldPos"></param>
        /// <returns></returns>
        public Vector2Int WorldToGrid(Vector2 worldPos)
        {
            return Tool_Grid.WorldToGrid(worldPos, cellCenter, cellSize, cellGap);
        }

        /// <summary>
        /// 网格坐标转化为世界坐标
        /// </summary>
        /// <param name="gridPos"></param>
        /// <returns></returns>
        public Vector2 GridToWorld(Vector2Int gridPos)
        {
            return Tool_Grid.GridToWorld(gridPos, cellCenter, cellSize, cellGap);
        }

        /// <summary>
        /// 返回世界坐标最接近的方格中心坐标
        /// </summary>
        /// <param name="worldPos"></param>
        /// <returns></returns>
        public Vector2 WorldToGridCenter(Vector2 worldPos)
        {
            return Tool_Grid.WorldToGridCenter(worldPos, cellCenter, cellSize, cellGap);
        }

        /// <summary>
        /// 删除指定单元坐标的Tile
        /// </summary>
        /// <param name="cellPos"></param>
        public void DestroyTileByCellPos(Vector2Int cellPos)
        {
            if (tileArray2D.IsOutOfCapacity(cellPos)) return;
            tileArray2D.GetByCoord(cellPos)?.DestroySelfImmediate();
            tileArray2D.SetByCoord(cellPos, null);
        }

        /// <summary>
        /// 在控制台打印当前地图中所有Tile的坐标
        /// </summary>
        [ContextMenu("Print All Tiles")]
        private void PrintAllTiles()
        {
            tileArray2D.PrintAllTiles();
        }

        private void Reset()
        {
            tileArray2D = new PrefabTileArray2D();
            grid = transform.parent.GetComponent<Grid>();
        }
    }
}