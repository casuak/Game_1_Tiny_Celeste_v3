using System;
using UnityEngine;

namespace TinyCeleste._05_MyTool._05_ArrayList2
{
    /// <summary>
    /// 自动扩容的二维列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class ArrayList2<T> where T : class
    {
        // 存储
        [SerializeField]
        private T[] m_Array;

        // 二维列表Size
        private Vector2Int m_Capacity;

        // 坐标原点对应的索引
        private Vector2Int m_OriginIndex;

        private void InitArray()
        {
            m_Array = new T[m_Capacity.x * m_Capacity.y];
        }

        public ArrayList2()
        {
            m_Capacity = new Vector2Int(2, 2);
            m_OriginIndex = new Vector2Int(0, 0);
            InitArray();
        }

        public ArrayList2(Vector2Int capacity)
        {
            m_Capacity = capacity;
            InitArray();
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

        public T Get(Vector2Int coord)
        {
            if (OutOfCapacity(coord)) return null;
            var index = CoordToIndex(coord);
            return m_Array[index.x + index.y * m_Capacity.x];
        }

        public void Set(Vector2Int coord, T obj)
        {
            if (OutOfCapacity(coord))
            {
                ExpandCapacity(coord);
            }

            var index = CoordToIndex(coord);
            m_Array[index.x + index.y * m_Capacity.x] = obj;
        }

        private bool OutOfCapacity(Vector2Int coord)
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
                increment.x = outIndex.x - m_Capacity.x + 1;
            }
            else if (outIndex.x < 0)
            {
                increment.x = -outIndex.x;
                newOriginIndex.x -= increment.x;
            }

            if (outIndex.y >= m_Capacity.y)
            {
                increment.y = outIndex.x - m_Capacity.y + 1;
            }
            else if (outIndex.y < 0)
            {
                increment.y = -outIndex.y;
                newOriginIndex.y -= increment.y;
            }

            var newCapacity = m_Capacity + increment;
            var newArray = new T[newCapacity.x * newCapacity.y];
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