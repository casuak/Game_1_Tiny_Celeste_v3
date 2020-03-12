#if UNITY_EDITOR

using UnityEngine;

namespace TinyCeleste._02_Modules._05_Env._01_Map._02_Brush
{
    public class Eraser : Brush
    {
        public Eraser()
        {
            icon = "Grid.EraserTool";
        }

        public override void OnMouseDown()
        {
            mapEditor.DestroyMapElement(mapEditor.mouseGridPos);
        }

        public override void OnGridPosChange()
        {
            if (mapEditor.isMouseDown)
            {
                mapEditor.DestroyMapElement(mapEditor.mouseGridPos);
            }
        }
    }
}

#endif