using UnityEngine;

namespace TinyCeleste._06_Plugins._01_PrefabTileMap._01_Brush._01_RectBrush
{
    public class RectEraser : RectBrush
    {
        protected override void RectOperation(Vector2Int cellPos)
        {
            map.DestroyTileByCellPos(cellPos);
        }
    }
}