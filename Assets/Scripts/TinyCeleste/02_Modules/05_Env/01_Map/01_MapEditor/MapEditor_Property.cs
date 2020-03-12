using Casuak.MyTool._04_Editor;
#if UNITY_EDITOR
using UnityEngine;

namespace TinyCeleste._02_Modules._05_Env._01_Map._01_MapEditor
{
    public partial class MapEditor
    {
        // 当前在场景中选中的MapEditor
        public static MapEditor ActiveInstance
        {
            get { return activeInstance; }
        }

        // 落入邻近方块中心的鼠标世界坐标
        public Vector2 mouseWorldPos
        {
            get { return map.WorldToSquareCenter(Tool_GUI.GetMouseWorldPosition()); }
        }

        // 当前是否处于编辑状态
        public bool isInEditMode
        {
            get { return currentBrush != null; }
        }

        // 当前选择的颜料
        public E_MapElement currentMapElement
        {
            get
            {
                if (currentMapElementIndex >= map.mapElementList.Count) return null;
                return map.mapElementList[currentMapElementIndex];
            }
        }
    }
}

#endif