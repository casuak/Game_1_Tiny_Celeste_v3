#if UNITY_EDITOR

using UnityEngine;

namespace TinyCeleste._02_Modules._05_Env._01_Map._02_Brush
{
    public class Pencil : Brush
    {
        // 笔头
        private E_MapElement pencilHead;

        public Pencil()
        {
            icon = "Grid.PaintTool";
        }

        // 刷新笔头位置
        private void RefreshHeadPosition(Vector2Int oldPos, Vector2Int newPos)
        {
            if (pencilHead != null)
            {
                mapEditor.MoveMapElement(oldPos, newPos);
            }
        }

        // 重新生成笔头
        private void ReGeneratePencilHead()
        {
            DestroyPencilHead();
            if (mapEditor.currentMapElement != null)
            {
                pencilHead = mapEditor.CreateMapElement(mapEditor.mouseGridPos, mapEditor.currentMapElement);
            }
        }

        // 销毁笔头
        public void DestroyPencilHead()
        {
            if (pencilHead != null)
                mapEditor.DestroyMapElement(pencilHead.gridPos);
            pencilHead = null;
        }
        
        // 落笔
        private void Paint()
        {
            if (pencilHead != null)
                mapEditor.DestroyHideElement(pencilHead.gridPos);
            pencilHead = null;
            ReGeneratePencilHead();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void OnMouseDown()
        {
            Paint();
        }

        public override void OnGridPosChange()
        {
            RefreshHeadPosition(mapEditor.lastMouseGridPos, mapEditor.mouseGridPos);
            if (mapEditor.isMouseDown) Paint();
        }

        public override void OnEnter()
        {
            ReGeneratePencilHead();
        }

        public override void OnExit()
        {
            DestroyPencilHead();
        }

        public override void OnSelectedColorChange()
        {
            ReGeneratePencilHead();
        }
    }
}

#endif