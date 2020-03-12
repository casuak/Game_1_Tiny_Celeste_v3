#if UNITY_EDITOR
using TinyCeleste._02_Modules._05_Env._01_Map._01_MapEditor;
using UnityEditor;
using UnityEngine;

namespace TinyCeleste._02_Modules._05_Env._01_Map._02_Brush
{
    public class Brush
    {
        // 笔刷名字
        public string name = "Brush";

        // 提示
        public string tip = "This is a tip.";

        // unity内置icon，可通过EditorGUIUtility.FindTexture获取
        public string icon = "BuildSettings.SelectedIcon";

        protected MapEditor mapEditor => MapEditor.ActiveInstance;

        protected E_Map map => mapEditor.map;

        protected Vector2Int mouseGridPos => mapEditor.mouseGridPos;
        
        public void LineStyle()
        {
            Handles.color = new Color(1f, 1f, 1f, 0.85f);
        }

        public Brush()
        {
        }

        public Brush(string name)
        {
            this.name = name;
        }

        // 鼠标按下时触发，只触发依次
        public virtual void OnMouseDown()
        {
        }

        // 鼠标放开时触发，只触发依次
        public virtual void OnMouseUp()
        {
        }

        // 鼠标按下时持续触发
        public virtual void OnMouseDrag()
        {
        }

        // 常态触发（预绘制Tile）
        public virtual void Update()
        {
            // 绘制外框
            LineStyle();
            Handles.DrawWireCube(mapEditor.mouseWorldPos, map.gridSize);
        }

        // 鼠标按下时触发
        public virtual void OnMouseDowning()
        {
        }

        // 鼠标移动时触发
        public virtual void OnMouseMove()
        {
        }

        /// 1、切换笔刷
        /// 2、退出编辑状态
        public virtual void OnExit()
        {
        }

        public virtual void OnEnter()
        {
        }

        // 选择的颜料发生改变
        public virtual void OnSelectedColorChange()
        {
        }

        // 所在方格变化时
        public virtual void OnGridPosChange()
        {
        }
    }
}

#endif