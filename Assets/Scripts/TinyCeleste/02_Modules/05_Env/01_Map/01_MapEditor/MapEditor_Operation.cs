
using TinyCeleste._02_Modules._05_Env._01_Map._03_Utils;
#if UNITY_EDITOR
using Casuak.Extension._01_UnityComponent;
using UnityEditor;
using UnityEngine;

namespace TinyCeleste._02_Modules._05_Env._01_Map._01_MapEditor
{
    /// <summary>
    /// 提供操作函数：
    /// 1、创建一个新的MapElement到Map中，可以指定是否暂时隐藏目标位置的MapElement
    /// 2、将一个MapElement移动到新的位置，可以指定是否暂时隐藏目标位置的MapElement
    /// 3、彻底删除一个MapElement
    /// 4、指定位置恢复显示
    /// </summary>
    public partial class MapEditor
    {
        public MapElementArray2 elementDic => map.elementDic;

        public MapElementArray2 hideDic => map.hideDic;

        public void ClearMap()
        {
            elementDic.ResetArray();
            hideDic.ResetArray();
            map.mapElementHolder.ClearChildren(true);
            currentBrush?.OnEnter();
        }

        private Vector2Int WorldToGrid(Vector2 worldPos)
        {
            return map.WorldToGrid(worldPos);
        }

        private Vector2 GridToWorld(Vector2Int gridPos)
        {
            return map.GridToWorld(gridPos);
        }

        // 创建并放置一个MapElement到地图
        public E_MapElement CreateMapElement(Vector2Int gridPos, E_MapElement prefab)
        {
            E_MapElement mapElement = PrefabUtility.InstantiatePrefab(prefab) as E_MapElement;
            return PutMapElement(gridPos, mapElement);
        }

        // 放置一个MapElement到地图
        private E_MapElement PutMapElement(Vector2Int gridPos, E_MapElement mapElement)
        {
            HideMapElement(gridPos);
            elementDic.Set(gridPos, mapElement);
            mapElement.SetParent(map.mapElementHolder)
                .SetPosition(GridToWorld(gridPos))
                .SetMap(map)
                .SetGridPos(gridPos);
            return mapElement;
        }

        // 移动
        public void MoveMapElement(Vector2Int origin, Vector2Int target)
        {
            var mapElement = elementDic.Get(origin);
            elementDic.Set(origin, null);
            ShowMapElement(origin);
            HideMapElement(target);
            elementDic.Set(target, mapElement);
            if (mapElement != null)
                mapElement.SetGridPos(target).SetPosition(GridToWorld(target));
        }

        // 隐藏MapElement
        private void HideMapElement(Vector2Int gridPos)
        {
            var mapElement = elementDic.Get(gridPos);
            if (mapElement != null)
            {
                mapElement.gameObject.SetActive(false);
                elementDic.Set(gridPos, null);
                hideDic.Set(gridPos, mapElement);
            }
        }

        // 显示MapElement
        private void ShowMapElement(Vector2Int gridPos)
        {
            var mapElement = hideDic.Get(gridPos);
            if (mapElement != null)
            {
                mapElement.gameObject.SetActive(true);
                hideDic.Set(gridPos, null);
                elementDic.Set(gridPos, mapElement);
            }
        }

        public void DestroyMapElement(Vector2Int gridPos)
        {
            var mapElement = elementDic.Get(gridPos);
            elementDic.Set(gridPos, null);
            if (mapElement != null) DestroyImmediate(mapElement.gameObject);

            // 同时恢复原本隐藏的MapElement
            ShowMapElement(gridPos);
        }

        public void DestroyHideElement(Vector2Int gridPos)
        {
            var hideElement = hideDic.Get(gridPos);
            hideDic.Set(gridPos, null);
            if (hideElement != null) DestroyImmediate(hideElement.gameObject);
        }
    }
}
#endif