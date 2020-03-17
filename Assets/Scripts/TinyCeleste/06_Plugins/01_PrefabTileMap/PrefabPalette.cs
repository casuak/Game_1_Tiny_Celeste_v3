using System.Collections.Generic;
using TinyCeleste._05_MyTool._03_WinExplorer;
using UnityEditor;
using UnityEngine;

namespace TinyCeleste._06_Plugins._01_PrefabTileMap
{
    /// <summary>
    /// 调色板，持久化存储的资源
    /// </summary>
    [CreateAssetMenu(fileName = "Prefab Palette.asset", menuName = "Custom/Palette")]
    public class PrefabPalette : ScriptableObject
    {
        /// <summary>
        /// 瓦片预制体列表
        /// </summary>
        public List<E_PrefabTile> prefabTiles;

        /// <summary>
        /// 获取当前项目下所有的调色板
        /// </summary>
        /// <returns></returns>
        public static PrefabPalette[] GetAllPalettes()
        {
            return Resources.LoadAll<PrefabPalette>("");
        }

        /// <summary>
        /// 在指定位置创建Asset文件（会自动覆盖同名文件）
        /// </summary>
        /// <returns></returns>
        [MenuItem("Casuak/Prefab Tile Map/Create Palette")]
        public static PrefabPalette CreatePalette()
        {
            var palette = CreateInstance<PrefabPalette>();
            var path = LocalDialog.GetFilePath("Prefab Palette");
            if (path == null) return null;
            AssetDatabase.CreateAsset(palette, path);
            return palette;
        }
    }
}