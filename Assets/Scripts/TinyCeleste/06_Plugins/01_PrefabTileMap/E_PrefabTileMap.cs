using System;
using Boo.Lang;
using TinyCeleste._01_Framework;
using TinyCeleste._04_Extension._01_UnityComponent;
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
        public Vector2 cellCenter => transform.position;

        /// <summary>
        /// 标准化的鼠标坐标
        /// </summary>
        public Vector2 normalizedMousePos => WorldToGridCenter(Tool_GUI.GetMouseWorldPosition());

        /// <summary>
        /// 网格中的鼠标坐标
        /// </summary>
        public Vector2Int mouseCellPos => WorldToGrid(Tool_GUI.GetMouseWorldPosition());

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
        public void DestroyTileImmediate(Vector2Int cellPos)
        {
            if (tileArray2D.IsOutOfCapacity(cellPos)) return;
            var tile = tileArray2D.GetByCoord(cellPos);
            if (tile != null)
                tile.DestroySelfImmediate();
            tileArray2D.SetByCoord(cellPos, null);
        }

        /// <summary>
        /// 在地图的指定位置由预制体创建Tile
        /// 同时删除原有的Tile
        /// </summary>
        /// <param name="cellPos"></param>
        /// <param name="prefab"></param>
        public void CreateTileAt(Vector2Int cellPos, E_PrefabTile prefab)
        {
            DestroyTileImmediate(cellPos);
            var tile = E_PrefabTile.Create(prefab).SetMap(this).SetCellPos(cellPos);
            tileArray2D.SetByCoord(cellPos, tile);
        }

        /// <summary>
        /// 根据网格重置Tile的坐标
        /// </summary>
        [ContextMenu("Reset Tile Position")]
        private void ResetTilePosition()
        {
            foreach (var tile in tileArray2D.array)
            {
                if (tile != null)
                {
                    tile.SetCellPos(tile.gridPos);
                }
            }
        }

        /// <summary>
        /// 通过网格坐标获取Tile
        /// </summary>
        /// <param name="cellPos"></param>
        /// <returns></returns>
        public E_PrefabTile GetTileByCellPos(Vector2Int cellPos)
        {
            return tileArray2D.GetByCoord(cellPos);
        }

        private void Reset()
        {
            tileArray2D = new PrefabTileArray2D();
            grid = transform.parent.GetComponent<Grid>();
        }

        private void OnDrawGizmosSelected()
        {
            // 算上间隔的单元格大小
            var cellSizeWithGap = cellSize + cellGap;
            // 地图的实际大小
            var mapSize = cellSizeWithGap * gridSize;
            // 地图的二维坐标
            var mapPos = transform.GetPos2D();
            // 原点坐标的位置
            var originCoord = mapPos + cellSize / 2;
            // 左下角单元格的坐标
            var leftBottom = originCoord + tileArray2D.originCoord * cellSizeWithGap;
            // 绘制边界
            var _leftBottom = leftBottom - cellSizeWithGap / 2;
            var _center = _leftBottom + mapSize / 2;
            Gizmos.color = new Color(1f, 1f, 1f, 0.8f);
            Gizmos.DrawWireCube(_center, mapSize);
        }
    }
}