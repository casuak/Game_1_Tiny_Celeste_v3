using TinyCeleste._06_Plugins._01_PrefabTileMap._02_PrefabPaletteWindow;
using UnityEditor;
using UnityEngine;

namespace TinyCeleste._06_Plugins._01_PrefabTileMap._01_Brush
{
    public partial class Brush
    {
        /// <summary>
        /// 笔刷名
        /// </summary>
        public string name = "Brush";

        /// <summary>
        /// 提示
        /// </summary>
        public string tip = "This is a brush.";

        /// <summary>
        /// unity内置图标
        /// </summary>
        public string icon = "BuildSettings.SelectedIcon";

        /// <summary>
        /// 默认的线框颜色
        /// </summary>
        public Color wireframeColor = new Color(1f, 1f, 1f, 0.85f);

        /// <summary>
        /// 所属窗口
        /// </summary>
        protected PrefabPaletteWindow window;

        /// <summary>
        /// 调色板窗口当前选中的tileMap
        /// </summary>
        protected E_PrefabTileMap map => window.currentMap;

        /// <summary>
        /// 上一帧鼠标的网格坐标
        /// </summary>
        protected Vector2Int lastMouseGridPos;

        /// <summary>
        /// 设置所属的窗口
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        public Brush SetWindow(PrefabPaletteWindow window)
        {
            this.window = window;
            return this;
        }

        /// <summary>
        /// 绘制提示外框
        /// </summary>
        public void DrawOutWire()
        {
            // 默认绘制选择的线框
            Handles.color = wireframeColor;
            Handles.DrawWireCube(map.normalizedMousePos, map.cellSize);
        }

        /// <summary>
        /// 触发鼠标单元格坐标变化事件
        /// </summary>
        public void TriggerMouseCellPosChange()
        {
            var mouseGridPos = map.mouseCellPos;
            if (mouseGridPos != lastMouseGridPos)
            {
                if (window.isMouseDowning) OnMouseCellPosChange();
                lastMouseGridPos = mouseGridPos;
            }
        }
    }
}