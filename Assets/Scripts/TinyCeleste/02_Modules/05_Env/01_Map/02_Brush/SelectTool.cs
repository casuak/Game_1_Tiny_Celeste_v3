using UnityEditor;

#if UNITY_EDITOR

namespace TinyCeleste._02_Modules._05_Env._01_Map._02_Brush
{
    /// <summary>
    /// 选中鼠标点击的物品
    /// </summary>
    public class SelectTool : Brush
    {
        public SelectTool()
        {
            icon = "Grid.Default";
        }

        public override void OnMouseDown()
        {
            // 退出编辑状态
            mapEditor.OnExitEditMode();

            // 选中物体
            var mapElement = mapEditor.elementDic.Get(mouseGridPos);
            if (mapElement != null)
                Selection.activeObject = mapElement;
        }
    }
}

#endif