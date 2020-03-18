using UnityEditor;

namespace TinyCeleste._06_Plugins._01_PrefabTileMap._02_PrefabPaletteWindow
{
    public partial class PrefabPaletteWindow
    {
        /// <summary>
        /// 进入编辑模式时调用
        /// </summary>
        private void OnEnterEditMode()
        {
            isMouseDowning = false;
            Tools.current = Tool.None;
        }

        /// <summary>
        /// 退出编辑模式时调用
        /// </summary>
        private void OnExitEditMode()
        {
            isMouseDowning = false;
            currentBrush?.OnExit();
            currentBrush = null;
            Repaint();
        }

        /// <summary>
        /// 当所选择的Tile发生变化时触发
        /// </summary>
        private void OnTileChanged()
        {
            currentBrush?.OnTileChanged();
        }
    }
}