using UnityEditor;
using UnityEngine;

namespace TinyCeleste._06_Plugins._01_PrefabTileMap._01_Brush
{
    public class Pointer : Brush
    {
        public Pointer()
        {
            icon = "Grid.Default";
        }

        public override void OnMouseDown()
        {
            window.ExitEditMode();
            Selection.activeObject = map.GetTileByCellPos(map.mouseCellPos);
        }
    }
}