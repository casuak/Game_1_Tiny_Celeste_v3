using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TinyCeleste._06_Plugins._01_PrefabTileMap
{
    /// <summary>
    /// 可序列化、可自动扩充容量的二维瓦片数组
    /// </summary>
    [Serializable]
    public class PrefabTileArray2D
    {
        /// <summary>
        /// 实际存储的数组（因unity序列化限制，用一维数组存储）
        /// </summary>
        [SerializeField]
        public E_PrefabTile[] array;

        /// <summary>
        /// 二维数组的大小
        /// </summary>
        [SerializeField]
        public Vector2Int capacity;

        /// <summary>
        /// 外部原点坐标对应的数组索引
        /// </summary>
        [SerializeField]
        private Vector2Int originIndex;

        /// <summary>
        /// 内部数组原点对应的外部坐标
        /// </summary>
        public Vector2Int originCoord => -originIndex;

        /// <summary>
        /// 容量不足时每次超扩充的容量大小
        /// 解释: 在满足扩充的基础需求下，进一步扩充的大小，从而适当减少扩充的频率
        /// </summary>
        [SerializeField]
        private Vector2Int increment;

        /// <summary>
        /// 构造函数
        /// </summary>
        public PrefabTileArray2D()
        {
            capacity = new Vector2Int(10, 10);
            originIndex = new Vector2Int(5, 5);
            increment = new Vector2Int(10, 10);
            array = new E_PrefabTile[capacity.x * capacity.y];
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="capacity"></param>
        /// <param name="originIndex"></param>
        public PrefabTileArray2D(Vector2Int capacity, Vector2Int originIndex)
        {
            this.capacity = capacity;
            this.originIndex = originIndex;
            increment = new Vector2Int(10, 10);
            array = new E_PrefabTile[capacity.x * capacity.y];
        }

        /// <summary>
        /// 将外部空间的整数坐标准换到内部二维数组空间的整数坐标
        /// </summary>
        /// <param name="coord"></param>
        /// <returns></returns>
        private Vector2Int CoordToIndex(Vector2Int coord)
        {
            return originIndex + coord;
        }

        /// <summary>
        /// 将内部二维数组空间的整数坐标准换到外部空间的整数坐标
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private Vector2Int IndexToCoord(Vector2Int index)
        {
            return index - originIndex;
        }

        /// <summary>
        /// 通过外部空间的坐标获取指定位置的Tile
        /// </summary>
        /// <param name="coord"></param>
        /// <returns></returns>
        public E_PrefabTile GetByCoord(Vector2Int coord)
        {
            if (IsOutOfCapacity(coord)) return null;
            var index = CoordToIndex(coord);
            return array[index.x + index.y * capacity.x];
        }

        /// <summary>
        /// 通过外部空间的坐标设置指定位置的Tile
        /// 当超出当前空间边界时，进行自动扩容
        /// </summary>
        /// <param name="coord"></param>
        /// <param name="tile"></param>
        public void SetByCoord(Vector2Int coord, E_PrefabTile tile)
        {
            if (IsOutOfCapacity(coord))
            {
                ExpandCapacity(coord);
            }

            var index = CoordToIndex(coord);
            array[index.x + index.y * capacity.x] = tile;
        }

        /// <summary>
        /// 判断外部空间的坐标是否超出当前数组的边界
        /// </summary>
        /// <param name="coord"></param>
        /// <returns></returns>
        public bool IsOutOfCapacity(Vector2Int coord)
        {
            var index = CoordToIndex(coord);
            return index.x < 0 || index.x >= capacity.x || index.y < 0 || index.y >= capacity.y;
        }

        /// <summary>
        /// 每个维度单方向扩容至可以容纳outIndex索引
        /// 并保持原有外部坐标不发生变化
        /// </summary>
        /// <param name="outCoord"></param>
        private void ExpandCapacity(Vector2Int outCoord)
        {
            var outIndex = CoordToIndex(outCoord);
            // 计算扩充后的数组长宽及外部原点
            var newOriginIndex = originIndex;
            var sumIncrement = new Vector2Int();
            if (outIndex.x >= capacity.x)
            {
                sumIncrement.x = outIndex.x - capacity.x + 1 + increment.x;
            }
            else if (outIndex.x < 0)
            {
                sumIncrement.x = -outIndex.x + increment.x;
                newOriginIndex.x += sumIncrement.x;
            }

            if (outIndex.y >= capacity.y)
            {
                sumIncrement.y = outIndex.y - capacity.y + 1 + increment.y;
            }
            else if (outIndex.y < 0)
            {
                sumIncrement.y = -outIndex.y + increment.y;
                newOriginIndex.y += sumIncrement.y;
            }

            var newCapacity = capacity + sumIncrement;
            var newArray = new E_PrefabTile[newCapacity.x * newCapacity.y];
            // 转移原有的Tile
            for (int x = 0; x < newCapacity.x; x++)
            {
                for (int y = 0; y < newCapacity.y; y++)
                {
                    newArray[x + y * newCapacity.x] = GetByCoord(new Vector2Int(x, y) - newOriginIndex);
                }
            }

            // 更新属性
            array = newArray;
            capacity = newCapacity;
            originIndex = newOriginIndex;
        }

        /// <summary>
        /// 销毁指定坐标的Tile
        /// </summary>
        /// <param name="coord"></param>
        public void DestroyTileByCoord(Vector2Int coord)
        {
            var tile = GetByCoord(coord);
            Object.DestroyImmediate(tile);
            SetByCoord(coord, null);
        }

        /// <summary>
        /// 删除矩形范围内的Tile
        /// </summary>
        /// <param name="leftBottom"></param>
        /// <param name="range"></param>
        public void DestroyTileByRange(Vector2Int leftBottom, Vector2Int range)
        {
            for (int y = leftBottom.y; y < range.y; y++)
            {
                for (int x = leftBottom.x; x < range.x; x++)
                {
                    DestroyTileByCoord(new Vector2Int(x, y));
                }
            }
        }

        /// <summary>
        /// 删除所有Tile
        /// </summary>
        public void DestroyAllTile()
        {
            DestroyTileByRange(originCoord, capacity);
        }

        /// <summary>
        /// 打印所有的地图元素
        /// </summary>
        public void PrintAllTiles()
        {
            foreach (var tile in array)
            {
                if (tile != null) Debug.Log(tile.gridPos);
            }
        }
    }
}