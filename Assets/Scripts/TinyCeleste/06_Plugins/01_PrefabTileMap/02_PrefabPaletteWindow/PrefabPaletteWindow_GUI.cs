using TinyCeleste._03_Editor;
using UnityEditor;
using UnityEngine;

namespace TinyCeleste._06_Plugins._01_PrefabTileMap._02_PrefabPaletteWindow
{
    public partial class PrefabPaletteWindow
    {
        /// <summary>
        /// 绘制笔刷工具栏
        /// </summary>
        public void GUI_BrushToolBar()
        {
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            // 设置content
            var brushContents = new GUIContent[brushList.Count];
            for (int i = 0; i < brushContents.Length; i++)
            {
                brushContents[i] = new GUIContent(EditorGUIUtility.FindTexture(brushList[i].icon));
            }

            // 绘制toolbar
            for (int i = 0; i < brushContents.Length; i++)
            {
                var style = UnityGUIStyles.buttonMid;
                if (brushContents.Length == 1)
                {
                    style = UnityGUIStyles.button;
                }
                else if (i == 0)
                {
                    style = UnityGUIStyles.buttonLeft;
                }
                else if (i == brushContents.Length - 1)
                {
                    style = UnityGUIStyles.buttonRight;
                }

                EditorGUI.BeginChangeCheck();
                GUILayout.Toggle(currentBrush == brushList[i], brushContents[i], style);
                if (EditorGUI.EndChangeCheck())
                {
                    if (currentBrush == null) OnEnterEditMode();
                    currentBrush?.OnExit();
                    currentBrush = currentBrush == brushList[i] ? null : brushList[i];
                    currentBrush?.OnEnter();
                    if (currentBrush == null) OnExitEditMode();
                    SceneView.RepaintAll();
                }
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// 选择调色板
        /// </summary>
        private void GUI_ChoosePalette()
        {
            // 选择Palette
            allPalettes = PrefabPalette.GetAllPalettes();
            var paletteNames = new string[allPalettes.Length];
            for (int i = 0; i < paletteNames.Length; i++)
            {
                paletteNames[i] = allPalettes[i].name;
            }

            if (paletteNames.Length == 0) paletteNames = new[] {"No Palette !"};
            GUI.SetNextControlName("popup_01");
            EditorGUI.BeginChangeCheck();
            currentPaletteIndex = EditorGUILayout.Popup(currentPaletteIndex, paletteNames,
                UnityGUIStyles.toolbarDropDown, GUILayout.Width(160));
            if (EditorGUI.EndChangeCheck())
            {
                // 重置当前选择的TileIndex为0
                currentTileIndex = 0;
            }

            // 新建Palette
            if (GUILayout.Button("Create Palette", UnityGUIStyles.toolbarButton, GUILayout.Width(100)))
            {
                PrefabPalette.CreatePalette();
            }
        }

        /// <summary>
        /// 选择编辑的Map
        /// </summary>
        private void GUI_ChooseMap()
        {
            UpdateAllMaps();
            // 文字提示
            GUILayout.Label("Active Map", UnityGUIStyles.toolbarButton, GUILayout.Width(90));
            // 下拉选项
            var tileMapNames = new string[allMaps.Length];
            for (int i = 0; i < tileMapNames.Length; i++)
            {
                tileMapNames[i] = allMaps[i].name;
            }

            if (tileMapNames.Length == 0) tileMapNames = new[] {"No Tile Map !"};
            GUI.SetNextControlName("popup_02");
            int newMapIndex = EditorGUILayout.Popup(currentMapIndex, tileMapNames,
                UnityGUIStyles.toolbarDropDown, GUILayout.Width(170));
            if (newMapIndex != currentMapIndex)
            {
                currentBrush?.OnExit();
                currentMapIndex = newMapIndex;
            }
        }

        /// <summary>
        /// 选择Tile
        /// </summary>
        private void GUI_ChooseTile()
        {
            var contents = new GUIContent[prefabTiles.Count];
            for (int i = 0; i < prefabTiles.Count; i++)
            {
                contents[i] = new GUIContent(prefabTiles[i].name);
            }

            int xCount = (int) position.width / 200;
            int yCount = prefabTiles.Count / xCount + 1;
            int height = yCount * 25;
            GUILayout.Space(3);
            GUILayout.BeginHorizontal();
            currentTileIndex = GUILayout.SelectionGrid(currentTileIndex, contents, xCount, GUILayout.Height(height));
            GUILayout.EndHorizontal();
        }
    }
}