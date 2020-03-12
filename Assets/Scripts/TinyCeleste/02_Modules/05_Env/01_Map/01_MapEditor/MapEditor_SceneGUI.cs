#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace TinyCeleste._02_Modules._05_Env._01_Map._01_MapEditor
{
    public partial class MapEditor
    {
        // 在场景中绘制网格
        private void SceneGUI_Grid()
        {
//            var gridSize = elementDic
//            var cellSize = map.gridSize;
//            var size = cellSize * gridSize;
//            var offset = map.position - new Vector2(cellSize.x / 2, cellSize.y / 2) * gridSize;
//            float a = 1 / SceneView.lastActiveSceneView.camera.orthographicSize;
//            Handles.color = new Color(1f, 1f, 1f, a);
//            // 横线
//            for (int y = 0; y <= cellSize.y; y++)
//            {
//                var p1 = new Vector2(0, y * gridSize.y) + offset;
//                var p2 = new Vector2(size.x, y * gridSize.y) + offset;
////                Handles.DrawLine(p1, p2);
//            }
//
//            // 竖线
//            for (int x = 0; x <= cellSize.x; x++)
//            {
//                var p1 = new Vector2(x * gridSize.x, 0) + offset;
//                var p2 = new Vector2(x * gridSize.x, size.y) + offset;
////                Handles.DrawLine(p1, p2);
//            }
        }

        // 处理场景中的鼠标动作
        private void SceneGUI_HandleMouseEvent()
        {
            Event e = Event.current;
            if (e.keyCode == KeyCode.Escape)
            {
                OnExitEditMode();
            }
            if (e.button == 0)
            {
                if (e.type == EventType.MouseMove)
                {
                    currentBrush?.OnMouseMove();
                }
                
                if (e.type == EventType.MouseUp)
                {
                    currentBrush?.OnMouseUp();
                    isMouseDown = false;
                }

                if (e.type == EventType.MouseLeaveWindow)
                {
                    isMouseDown = false;
                }

                if (e.type == EventType.MouseDown)
                {
                    currentBrush?.OnMouseDown();
                    isMouseDown = true;
                }

                if (e.type == EventType.MouseDrag)
                {
                    currentBrush?.OnMouseDrag();
                }

                if (isMouseDown)
                {
                    currentBrush?.OnMouseDowning();
                }
            }
        }
    }
}

#endif