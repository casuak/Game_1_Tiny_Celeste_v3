using System;
using UnityEngine;

namespace TinyCeleste._02_Modules._05_Env._01_Map._03_Utils
{
    [Serializable]
    public class MapElementArray2
    {
        // 存储
        [SerializeField]
        private E_MapElement[] m_Array;

        // 二维列表Size
        [SerializeField]
        private Vector2Int m_Capacity;

        // 坐标原点对应的索引
        [SerializeField]
        private Vector2Int m_OriginIndex;

        // 多余增加
        [SerializeField]
        private Vector2Int m_Increment;
        
        // 容量
        public Vector2Int capacity
        {
            get => m_Capacity;
        }

        public MapElementArray2()
        {
            ResetArray();
        }

        public void ResetArray()
        {
            m_Capacity = new Vector2Int(10, 10);
            m_OriginIndex = new Vector2Int(4, 4);
            m_Increment = new Vector2Int(10, 10);
            m_Array = new E_MapElement[m_Capacity.x * m_Capacity.y];
        }

        /// <summary>
        /// 外部空间转换到索引空间
        /// </summary>
        /// <param name="coord"></param>
        /// <returns></returns>
        private Vector2Int CoordToIndex(Vector2Int coord)
        {
            return coord + m_OriginIndex;
        }

        private Vector2Int IndexToCoord(Vector2Int index)
        {
            return index -= m_OriginIndex;
        }

        public E_MapElement Get(Vector2Int coord)
        {
            if (OutOfCapacity(coord)) return null;
            var index = CoordToIndex(coord);
            return m_Array[index.x + index.y * m_Capacity.x];
        }

        public void Set(Vector2Int coord, E_MapElement mapElement)
        {
            if (OutOfCapacity(coord))
            {
                ExpandCapacity(coord);
            }

            var index = CoordToIndex(coord);
            m_Array[index.x + index.y * m_Capacity.x] = mapElement;
        }

        public bool OutOfCapacity(Vector2Int coord)
        {
            var index = CoordToIndex(coord);
            return index.x < 0 || index.x >= m_Capacity.x || index.y < 0 || index.y >= m_Capacity.y;
        }

        // 单边扩容至可以容纳outIndex索引
        private void ExpandCapacity(Vector2Int outCoord)
        {
            var outIndex = CoordToIndex(outCoord);
            // 计算扩充后的长宽以及原点在索引空间的坐标
            var newOriginIndex = m_OriginIndex;
            var increment = new Vector2Int();
            if (outIndex.x >= m_Capacity.x)
            {
                increment.x = outIndex.x - m_Capacity.x + 1 + m_Increment.x;
            }
            else if (outIndex.x < 0)
            {
                increment.x = -outIndex.x + m_Increment.x;
                newOriginIndex.x += increment.x;
            }

            if (outIndex.y >= m_Capacity.y)
            {
                increment.y = outIndex.y - m_Capacity.y + 1 + m_Increment.y;
            }
            else if (outIndex.y < 0)
            {
                increment.y = -outIndex.y + m_Increment.y;
                newOriginIndex.y += increment.y;
            }

            var newCapacity = m_Capacity + increment;
            var newArray = new E_MapElement[newCapacity.x * newCapacity.y];
            // 转移元素(外部坐标不变)
            for (int x = 0; x < newCapacity.x; x++)
            {
                for (int y = 0; y < newCapacity.y; y++)
                {
                    newArray[x + y * newCapacity.x] = Get(new Vector2Int(x, y) - newOriginIndex);
                }
            }

            m_Array = newArray;
            m_Capacity = newCapacity;
            m_OriginIndex = newOriginIndex;
        }
    }
}