using TinyCeleste._04_Extension._02_Unity;
using UnityEditor;
using UnityEngine;

namespace TinyCeleste._06_Plugins._01_PrefabTileMap._01_Brush._01_RectBrush
{
    public class RectBrush : Brush
    {
        /// <summary>
        /// 鼠标按下时的坐标
        /// </summary>
        protected Vector2Int cornerBegin;

        /// <summary>
        /// 鼠标释放时的坐标
        /// </summary>
        protected Vector2Int cornerEnd;

        /// <summary>
        /// 右键取消事件
        /// </summary>
        protected bool cancel;

        public RectBrush()
        {
            icon = "Grid.BoxTool";
        }

        /// <summary>
        /// 持续触发
        /// </summary>
        public override void Update()
        {
            // 鼠标按下时不再绘制提示外框
            if (!window.isMouseDowning)
                DrawOutWire();
            TriggerMouseCellPosChange();
        }

        /// <summary>
        /// 开始一个矩形操作
        /// </summary>
        public override void OnMouseDown()
        {
            cornerBegin = map.mouseGridPos;
        }

        /// <summary>
        /// 鼠标抬起，若无取消，则执行矩形操作
        /// </summary>
        public override void OnMouseUp()
        {
            if (cancel)
            {
                cancel = false;
                return;
            }

            var cellPositions = EX_Vector2Int.Range(cornerBegin, cornerEnd);
            for (int y = 0; y < cellPositions.Length; y++)
            {
                for (int x = 0; x < cellPositions[y].Length; x++)
                {
                    RectOperation(cellPositions[y][x]);
                }
            }
        }

        /// <summary>
        /// 逐单元的矩形操作
        /// </summary>
        /// <param name="cellPos"></param>
        protected virtual void RectOperation(Vector2Int cellPos)
        {
        }

        /// <summary>
        /// 右键中断矩形操作
        /// </summary>
        public override void OnRightMouseUp()
        {
            cancel = true;
        }

        /// <summary>
        /// 绘制范围网格
        /// </summary>
        public override void OnMouseDowning()
        {
            cornerEnd = map.mouseGridPos;
            var leftBottom = new Vector2Int();
            var rightTop = new Vector2Int();
            EX_Vector2Int.TwoCorner(ref leftBottom, ref rightTop, cornerBegin, cornerEnd);
            var _leftBottom = map.GridToWorld(leftBottom) - map.cellSize / 2;
            var _rightTop = map.GridToWorld(rightTop) + map.cellSize / 2;
            var _center = (_leftBottom + _rightTop) / 2;
            var _size = _rightTop - _leftBottom;
            Handles.color = wireframeColor;
            Handles.DrawWireCube(_center, _size);
        }
    }
}