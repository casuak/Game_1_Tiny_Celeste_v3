using System;
using System.Collections.Generic;
using TinyCeleste._03_Editor;
using TinyCeleste._06_Plugins._01_PrefabTileMap._01_Brush;
using TinyCeleste._06_Plugins._01_PrefabTileMap._01_Brush._01_RectBrush;
using UnityEditor;
using UnityEngine;

namespace TinyCeleste._06_Plugins._01_PrefabTileMap._02_PrefabPaletteWindow
{
    /// <summary>
    /// 调色板编辑窗口
    /// 为单例
    /// </summary>
    public partial class PrefabPaletteWindow : EditorWindow
    {
        /// <summary>
        /// 窗口打开时
        /// </summary>
        private void OnEnable()
        {
            SceneView.duringSceneGui += OnSceneGUI;
            Debug.Log("On enable prefab palette window.");
            ShowWindow();
        }

        /// <summary>
        /// 窗口关闭时
        /// </summary>
        private void OnDestroy()
        {
            SceneView.duringSceneGui -= OnSceneGUI;
            Debug.Log("On destroy prefab palette window.");
        }

        /// <summary>
        /// 窗口打开
        /// </summary>
        [MenuItem("Casuak/Prefab Tile Map/Palette Window")]
        private static void ShowWindow()
        {
            Debug.Log("On show window.");
            // 单例赋值
            var window = GetWindow<PrefabPaletteWindow>();
            
            // 初始化窗口属性
            window.titleContent = new GUIContent("Prefab Palette");
            window.scrollPosition = Vector2.zero;
            // 初始化笔刷
            window.brushList = new List<Brush>()
            {
                new Pencil(), new Eraser(), new RectPencil(), new RectEraser(), new Pointer()
            };
            foreach (var brush in window.brushList)
            {
                brush.SetWindow(window);
            }
            window.currentBrush = null;
            window.currentMapIndex = 0;
            window.currentPaletteIndex = 0;
            window.currentTileIndex = 0;
        }

        /// <summary>
        /// 更新
        /// </summary>
        private void Update()
        {
            // 退出编辑状态
            if (Tools.current != Tool.None && currentBrush != null)
            {
                currentBrush.OnExit();
                currentBrush = null;
                Repaint();
            }
        }

        /// <summary>
        /// 绘制窗口GUI
        /// </summary>
        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Is mouse downing: " + isMouseDowning, UnityGUIStyles.toolbarButton);
            EditorGUILayout.EndHorizontal();
            
            
            GUI_BrushToolBar();
            GUILayout.Space(2);
            EditorGUILayout.BeginHorizontal();
            GUI_ChoosePalette();
            GUILayout.FlexibleSpace();
            GUI_ChooseMap();
            EditorGUILayout.EndHorizontal();
            
            GUI_ChooseTile();

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 场景
        /// </summary>
        private void OnSceneGUI(SceneView view)
        {
            if (!isInEditMode) return;
            UpdateAllMaps();
            if (allMaps.Length == 0) return;
            // 绘制网格
            SceneGUI_Grid();
            // 绘制笔刷
            currentBrush?.Update();
            // 处理鼠标输入事件
            SceneGUI_HandleMouseEvent();
            // 强制更新场景内的UI
            SceneView.RepaintAll();
            // 屏蔽场景的内置行为
            // HandleUtility.AddDefaultControl —— 添加默认控件的ID。如果没有其他选择，这个将被选中
            // GUIUtility.GetControlID —— 获取控件的唯一ID，使用整数作为提示，以帮助确保ID与控件的正确匹配
            // FocusType.Passive 表示禁止接收控制焦点，获取它的 controlID 后，即可禁止将点击事件穿透下去
            // FocusType.Passive —— 此控件无法接收键盘焦点
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
        }
    }
}