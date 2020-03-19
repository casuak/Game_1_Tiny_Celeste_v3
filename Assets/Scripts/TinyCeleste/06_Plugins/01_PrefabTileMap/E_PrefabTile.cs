using TinyCeleste._01_Framework;
using TinyCeleste._04_Extension._01_UnityComponent;
using UnityEditor;
using UnityEngine;

namespace TinyCeleste._06_Plugins._01_PrefabTileMap
{
    /// <summary>
    /// 瓦片类
    /// </summary>
    public class E_PrefabTile : Entity
    {
        /// <summary>
        /// 当前所属的父地图
        /// </summary>
        public E_PrefabTileMap map;

        /// <summary>
        /// 网格坐标
        /// </summary>
        public Vector2Int gridPos;

        /// <summary>
        /// 是否处于临时隐藏的状态
        /// 发生时机: 铅笔笔刷划过当前地图元素时，会暂时将当前地图元素隐藏
        /// 若按下鼠标左键，则当前地图元素被彻底删除
        /// 若鼠标离开，则当前地图元素又被显示
        /// </summary>
        public bool isTempHide;

        /// <summary>
        /// 隐藏
        /// </summary>
        public void TempHide()
        {
            isTempHide = true;
            gameObject.SetActive(false);
        }

        /// <summary>
        /// 显示
        /// </summary>
        public void TempShow()
        {
            isTempHide = false;
            gameObject.SetActive(true);
        }

        /// <summary>
        /// 设置网格坐标，同时设置世界坐标
        /// </summary>
        /// <param name="gridPos"></param>
        /// <returns></returns>
        public E_PrefabTile SetCellPos(Vector2Int gridPos)
        {
            this.gridPos = gridPos;
            transform.SetPos2D(map.GridToWorld(gridPos));
            return this;
        }

        /// <summary>
        /// 销毁自身
        /// </summary>
        public void DestroySelfImmediate()
        {
            DestroyImmediate(gameObject);
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="prefab"></param>
        /// <returns></returns>
        public static E_PrefabTile Create(E_PrefabTile prefab)
        {
            if (prefab == null) return null;
            return (E_PrefabTile) PrefabUtility.InstantiatePrefab(prefab);
        }

        /// <summary>
        /// 设置所属的map，同时设置parent
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public E_PrefabTile SetMap(E_PrefabTileMap map)
        {
            this.map = map;
            transform.parent = map.transform;
            return this;
        }
    }
}