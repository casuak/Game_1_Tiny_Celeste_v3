#if UNITY_EDITOR

using TinyCeleste._03_Editor;
using UnityEditor;
using UnityEngine;

namespace TinyCeleste._02_Modules._05_Env._01_Map._01_MapEditor
{
    public partial class MapEditor
    {
        // 笔刷选择栏
        private void GUI_BrushGroup()
        {
            // 处理content
            var brushContents = new GUIContent[brushList.Count];
            for (int i = 0; i < brushContents.Length; i++)
            {
                brushContents[i] = new GUIContent(EditorGUIUtility.FindTexture(brushList[i].icon));
            }

            // 绘制toolbar
            EditorGUILayout.BeginHorizontal();
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
                }
            }

            EditorGUILayout.EndVertical();
        }

        // 选择颜料(MapElement)的地方
        private void GUI_SelectMapElement()
        {
            EditorGUILayout.BeginHorizontal();

            var popupNames = new string[map.mapElementList.Count];
            for (int i = 0; i < popupNames.Length; i++)
            {
                string name = "Element " + i + " is null";
                if (map.mapElementList[i] != null) name = map.mapElementList[i].name;
                popupNames[i] = name;
            }

            // 名字重复的会被省去
            EditorGUI.BeginChangeCheck();
            currentMapElementIndex = EditorGUILayout.Popup(currentMapElementIndex, popupNames,
                UnityGUIStyles.dropDown);
            if (EditorGUI.EndChangeCheck())
            {
                OnSelectedColorChange();
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}

#endif