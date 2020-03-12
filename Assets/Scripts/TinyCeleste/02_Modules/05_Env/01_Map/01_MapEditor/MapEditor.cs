#if UNITY_EDITOR

using System.Collections.Generic;
using TinyCeleste._02_Modules._05_Env._01_Map._02_Brush;
using UnityEditor;
using UnityEngine;

namespace TinyCeleste._02_Modules._05_Env._01_Map._01_MapEditor
{
    [CustomEditor(typeof(E_Map))]
    public partial class MapEditor : Editor
    {
        private void OnEnable()
        {
            map = target as E_Map;
            if (map == null) throw new UnityException("Fuck Map");
            map.mapEditor = this;

            activeInstance = this;
            // 初始化数据
            brushList = new List<Brush>()
            {
                new Pencil(), new Eraser(), new SelectTool(), new Brush(),
            };
            currentBrush = null;
            currentMapElementIndex = 0;

            EditorApplication.update += Update;
        }

        private void Update()
        {
            // 按外部工具退出编辑状态
            if (Tools.current != Tool.None && isInEditMode)
            {
                OnExitEditMode();
            }

            // 编辑状态中锁定Inspector面板
            if (isInEditMode)
            {
                Selection.activeObject = map.gameObject;
            }
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical();

            GUI_BrushGroup();
            EditorGUILayout.Space();

            GUI_SelectMapElement();
            EditorGUILayout.Space();

            base.OnInspectorGUI();

            EditorGUILayout.EndVertical();
        }

        private void OnSceneGUI()
        {
            if (map.grid == null) return;

            // 选中时即进行网格绘制
            SceneGUI_Grid();
            // 编辑状态下处理鼠标事件
            if (!isInEditMode) return;
            // 更新所在方格坐标
            mouseGridPos = map.WorldToGrid(mouseWorldPos);
            if (lastMouseGridPos != mouseGridPos)
            {
                currentBrush?.OnGridPosChange();
                lastMouseGridPos = mouseGridPos;
            }

            // 处理鼠标事件
            SceneGUI_HandleMouseEvent();
            // 绘制笔头
            currentBrush?.Update();
            SceneView.RepaintAll();
            // 屏蔽场景的内置行为
            // HandleUtility.AddDefaultControl —— 添加默认控件的ID。如果没有其他选择，这个将被选中
            // GUIUtility.GetControlID —— 获取控件的唯一ID，使用整数作为提示，以帮助确保ID与控件的正确匹配
            // FocusType.Passive 表示禁止接收控制焦点，获取它的 controlID 后，即可禁止将点击事件穿透下去
            // FocusType.Passive —— 此控件无法接收键盘焦点
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
        }

        private void OnDisable()
        {
            activeInstance = null;
            OnExitEditMode();
            EditorApplication.update -= Update;
        }
    }
}

#endif