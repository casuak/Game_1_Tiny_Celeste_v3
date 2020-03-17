using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

namespace TinyCeleste._05_MyTool._04_Editor
{
    public partial class Tool_GUI
    {
        /// <summary>
        /// 获取鼠标在世界坐标的位置
        /// </summary>
        /// <returns></returns>
        public static Vector2 GetMouseWorldPosition()
        {
            // 鼠标的GUI坐标
            var mousePosition = Event.current.mousePosition;
            return SceneViewPosToWorldPos(mousePosition);
        }

        /// <summary>
        /// 将场景视图中的坐标转换到世界空间中的坐标(2D)
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static Vector2 SceneViewPosToWorldPos(Vector2 position)
        {
            // GUI坐标到屏幕坐标
            position = HandleUtility.GUIPointToScreenPixelCoordinate(position);
            // 屏幕坐标到世界坐标
            var sceneView = SceneView.lastActiveSceneView;
            if (sceneView)
                position = sceneView.camera.ScreenToWorldPoint(position);
            else
                position = Vector2.zero;
            return position;
        }
    }
}

#endif