using TinyCeleste._04_Extension._01_UnityComponent;
using UnityEditor;
using UnityEngine;

namespace TinyCeleste._06_Plugins._01_PrefabTileMap._02_PrefabPaletteWindow
{
    public partial class PrefabPaletteWindow
    {
        /// <summary>
        /// 绘制网格
        /// </summary>
        private void SceneGUI_Grid()
        {
            var map = currentMap;
            // 单元格大小
            var cellSize = map.cellSize;
            // 单元格间隔
            var cellGap = map.cellGap;
            // 单元格的横纵大小
            var gridSize = map.gridSize;
            // 算上间隔的单元格大小
            var cellSizeWithGap = cellSize + cellGap;
            // 地图的实际大小
            var mapSize = cellSizeWithGap * gridSize;
            // 地图的二维坐标
            var mapPos = map.transform.GetPos2D();
            // 原点坐标的位置
            var originCoord = mapPos + cellSize / 2;
            // 左下角单元格的坐标
            var leftBottom = originCoord + map.tileArray2D.originCoord * cellSizeWithGap;
            float a = 1 / SceneView.lastActiveSceneView.camera.orthographicSize;
            Handles.color = new Color(1f, 1f, 1f, a);
            // 绘制单元格
//            for (int y = 0; y < gridSize.y; y++)
//            {
//                for (int x = 0; x < gridSize.x; x++)
//                {
//                    var center = leftBottom + new Vector2(x, y) * cellSizeWithGap;
//                    Handles.DrawWireCube(center, cellSize);
//                }
//            }
            // 绘制边界
            var _leftBottom = leftBottom - cellSizeWithGap / 2;
            var _center = _leftBottom + mapSize / 2;
            Handles.color = new Color(1f, 1f, 1f, 0.8f);
            Handles.DrawWireCube(_center, mapSize);
        }

        /// <summary>
        /// 处理鼠标输入事件
        /// </summary>
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
                    isMouseDowning = false;
                }

                if (e.type == EventType.MouseLeaveWindow)
                {
                    isMouseDowning = false;
                }

                if (e.type == EventType.MouseDown)
                {
                    currentBrush?.OnMouseDown();
                    isMouseDowning = true;
                }

                if (e.type == EventType.MouseDrag)
                {
                    currentBrush?.OnMouseDrag();
                }

                if (isMouseDowning)
                {
                    currentBrush?.OnMouseDowning();
                }
            }

            if (e.button == 1)
            {
                if (e.type == EventType.MouseUp)
                {
                    currentBrush?.OnRightMouseUp();
                }
            }
        }
    }
}