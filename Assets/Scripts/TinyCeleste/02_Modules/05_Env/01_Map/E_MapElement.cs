using TinyCeleste._01_Framework;
using UnityEngine;

namespace TinyCeleste._02_Modules._05_Env._01_Map
{
    /// <summary>
    /// 存储将prefab放置于时的地图的参数
    /// </summary>
    public class E_MapElement : Entity
    {
        public E_Map map;

        // 记录了MapElement在地图中的静态网格坐标
        public Vector2Int gridPos;

        public bool isHide;

        public void Hide()
        {
            isHide = true;
            gameObject.SetActive(!isHide);
        }

        public void Show()
        {
            isHide = false;
            gameObject.SetActive(!isHide);
        }

        public E_MapElement SetMap(E_Map map)
        {
            this.map = map;
            return this;
        }

        public E_MapElement SetParent(Transform parent)
        {
            transform.parent = parent;
            return this;
        }

        public E_MapElement SetGridPos(Vector2Int gridPos)
        {
            this.gridPos = gridPos;
            return this;
        }

        public E_MapElement SetPosition(Vector2 position)
        {
            transform.position = position;
            return this;
        }
    }
}