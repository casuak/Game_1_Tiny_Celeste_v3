using System;
using TinyCeleste._04_Extension._02_Unity;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TinyCeleste._06_Plugins._01_PrefabTileMap._01_Brush._01_RectBrush
{
    public class RectPencil : RectBrush
    {
        public RectPencil()
        {
            icon = "Grid.FillTool";
        }

        /// <summary>
        /// 清空临时二维数组内的tile
        /// </summary>
        private void ClearTempTileArray2D()
        {
            var array = window.tempTileArray2D;
            for (int y = 0; y < array.Length; y++)
            {
                for (int x = 0; x < array[y].Length; x++)
                {
                    Object.Destroy(array[y][x]);
                }
            }
        }

        /// <summary>
        /// 鼠标按下
        /// </summary>
        public override void OnMouseDown()
        {
            // 设置cornerBegin
            base.OnMouseDown();
            // 初始化临时存储数组
            window.tempTileArray2D = new E_PrefabTile[1][];
            window.tempTileArray2DOffset = map.mouseGridPos;
            window.tempTileArray2D[0] = new[]
            {
                E_PrefabTile.Create(window.currentTile).SetMap(map).SetGridPos(map.mouseGridPos)
            };
        }

        public override void OnMouseGridPosChange()
        {
            // 绘制范围网格以及更新cornerEnd
            base.OnMouseDowning();
            // 获取两个角落
            var leftBottom = new Vector2Int();
            var rightTop = new Vector2Int();
            EX_Vector2Int.TwoCorner(ref leftBottom, ref rightTop, cornerBegin, cornerEnd);
            var gridSize = rightTop - leftBottom + Vector2Int.one;
            // 生成新数组
            var tileArray2D = new E_PrefabTile[gridSize.y][];
            // 旧的参数
            var oldArray = window.tempTileArray2D;
            var oldSize = new Vector2Int(oldArray[0].Length, oldArray.Length);
            var oldOffset = window.tempTileArray2DOffset;
            var oldUseful = new bool[oldSize.y][];
            // 初始化利用表(不被利用的tile将被destroy)
            for (int y = 0; y < oldSize.y; y++)
            {
                oldUseful[y] = new bool[oldSize.x];
                for (int x = 0; x < oldSize.x; x++)
                {
                    oldUseful[y][x] = false;
                }
            }

            // 生成新的
            for (int y = 0; y < gridSize.y; y++)
            {
                tileArray2D[y] = new E_PrefabTile[gridSize.x];
                for (int x = 0; x < gridSize.x; x++)
                {
                    var gridPos = new Vector2Int(x, y) + leftBottom;
                    var arrayIndex = gridPos - oldOffset;
                    // 利用重叠部分
                    if (arrayIndex.x >= 0 && arrayIndex.x < oldSize.x && arrayIndex.y >= 0 && arrayIndex.y < oldSize.y)
                    {
                        var oldTile = oldArray[arrayIndex.y][arrayIndex.x];
                        tileArray2D[y][x] = oldTile;
                        oldTile.SetGridPos(gridPos);
                        oldUseful[arrayIndex.y][arrayIndex.x] = true;
                    }
                    // 生成未有部分
                    else
                    {
                        tileArray2D[y][x] = E_PrefabTile.Create(window.currentTile);
                        tileArray2D[y][x].SetMap(window.currentMap)
                            .SetGridPos(gridPos);
                    }
                }
            }

            // 删除未利用的
            for (int y = 0; y < oldSize.y; y++)
            {
                for (int x = 0; x < oldSize.x; x++)
                {
                    if (!oldUseful[y][x])
                    {
                        oldArray[y][x].DestroySelfImmediate();
                    }
                }
            }

            // 更新参数
            window.tempTileArray2D = tileArray2D;
            window.tempTileArray2DOffset = leftBottom;
        }

        /// <summary>
        /// 逐单元格将临时空间中的物体放入地图空间
        /// </summary>
        /// <param name="cellPos"></param>
        protected override void RectOperation(Vector2Int cellPos)
        {
            var arrayIndex = cellPos - window.tempTileArray2DOffset;
            var tile = window.tempTileArray2D[arrayIndex.y][arrayIndex.x];
            window.tempTileArray2D[arrayIndex.y][arrayIndex.x] = null;
            map.tileArray2D.GetByCoord(cellPos)?.DestroySelfImmediate();
            map.tileArray2D.SetByCoord(cellPos, tile);
        }

        /// <summary>
        /// 销毁临时空间中的Tile
        /// </summary>
        public override void OnMouseUp()
        {
            base.OnMouseUp();
            for (int y = 0; y < window.tempTileArray2D.Length; y++)
            {
                for (int x = 0; x < window.tempTileArray2D[y].Length; x++)
                {
                    window.tempTileArray2D[y][x]?.DestroySelfImmediate();
                }
            }
        }
    }
}