#if UNITY_EDITOR

using System.Collections.Generic;
using TinyCeleste._02_Modules._05_Env._01_Map._02_Brush;
using UnityEditor;
using UnityEngine;

namespace TinyCeleste._02_Modules._05_Env._01_Map._01_MapEditor
{
    public partial class MapEditor
    {
        // 当前场景中选中的MapEditor(全局唯一)
        private static MapEditor activeInstance;
        
        // 编辑的Map
        public E_Map map;

        // 笔刷列表
        private List<Brush> brushList;
        
        // 当前选择的笔刷
        public Brush currentBrush;
        
        // 当前选择的颜料索引
        private int currentMapElementIndex;
        
        // 鼠标是否按下
        public bool isMouseDown;
        
        // 记录鼠标所在方格坐标
        public Vector2Int mouseGridPos;

        // 上一帧的鼠标位置
        public Vector2Int lastMouseGridPos;
    }
}
#endif