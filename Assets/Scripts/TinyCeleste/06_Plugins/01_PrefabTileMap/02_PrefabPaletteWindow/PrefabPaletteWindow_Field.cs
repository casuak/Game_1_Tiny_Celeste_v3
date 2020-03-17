using System.Collections.Generic;
using TinyCeleste._06_Plugins._01_PrefabTileMap._01_Brush;
using UnityEngine;

namespace TinyCeleste._06_Plugins._01_PrefabTileMap._02_PrefabPaletteWindow
{
    public partial class PrefabPaletteWindow
    {
        /// <summary>
        /// 窗口滑动位置
        /// </summary>
        private Vector2 scrollPosition;

        /// <summary>
        /// 笔刷列表
        /// </summary>
        private List<Brush> brushList;

        /// <summary>
        /// 当前选择的笔刷
        /// </summary>
        private Brush currentBrush;

        /// <summary>
        /// 当前场景下的所有TileMap
        /// </summary>
        private E_PrefabTileMap[] allMaps;

        /// <summary>
        /// 当前项目下的所有Palette
        /// </summary>
        private PrefabPalette[] allPalettes;

        /// <summary>
        /// 当前选择的地图索引
        /// </summary>
        private int currentMapIndex;

        /// <summary>
        /// 当前选择的调色板索引
        /// </summary>
        private int currentPaletteIndex;

        /// <summary>
        /// 当前选择的地图
        /// </summary>
        public E_PrefabTileMap currentMap => currentMapIndex < allMaps.Length ? allMaps[currentMapIndex] : null;

        /// <summary>
        /// 当前选择的palette
        /// </summary>
        private PrefabPalette currentPalette => currentPaletteIndex < allPalettes.Length
            ? allPalettes[currentPaletteIndex]
            : null;

        /// <summary>
        /// 可供选择的Tile列表
        /// </summary>
        private List<E_PrefabTile> prefabTiles => currentPalette != null ? currentPalette.prefabTiles : null;

        /// <summary>
        /// 当前选择的Tile索引
        /// </summary>
        private int currentTileIndex;

        /// <summary>
        /// 当前选择的Tile
        /// </summary>
        public E_PrefabTile currentTile => (prefabTiles != null && currentTileIndex < prefabTiles.Count)
            ? prefabTiles[currentTileIndex]
            : null;

        /// <summary>
        /// 当前是否处于编辑状态
        /// </summary>
        public bool isInEditMode => currentBrush != null;
        
        /// <summary>
        /// 存储还未实际存储到map中的tile
        /// </summary>
        public E_PrefabTile[][] tempTileArray2D;

        /// <summary>
        /// 左下角对应的网格坐标
        /// 当前设计主要提供给矩形画笔工具使用
        /// </summary>
        public Vector2Int tempTileArray2DOffset;

        /// <summary>
        /// 鼠标左键是否被按下
        /// </summary>
        public bool isMouseDowning;
    }
}