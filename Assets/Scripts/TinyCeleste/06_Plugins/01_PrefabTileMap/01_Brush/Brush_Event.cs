using UnityEditor;

namespace TinyCeleste._06_Plugins._01_PrefabTileMap._01_Brush
{
    public partial class Brush
    {
        /// <summary>
        /// 鼠标左键按下触发，只触发一次
        /// </summary>
        public virtual void OnMouseDown()
        {
        }

        /// <summary>
        /// 鼠标左键放开时触发
        /// </summary>
        public virtual void OnMouseUp()
        {
        }

        /// <summary>
        /// 鼠标左键拖动时触发
        /// </summary>
        public virtual void OnMouseDrag()
        {
        }

        /// <summary>
        /// 鼠标左键按下，持续触发
        /// </summary>
        public virtual void OnMouseDowning()
        {
        }

        /// <summary>
        /// 鼠标左键移动
        /// </summary>
        public virtual void OnMouseMove()
        {
        }

        /// <summary>
        /// 鼠标右键抬起
        /// </summary>
        public virtual void OnRightMouseUp()
        {
        }

        /// <summary>
        /// 持续触发
        /// </summary>
        public virtual void Update()
        {
            DrawOutWire();
            TriggerMouseCellPosChange();
        }

        /// <summary>
        /// 退出当前笔刷时调用
        /// 切换笔刷或退出编辑状态或切换map时触发
        /// </summary>
        public virtual void OnExit()
        {
        }

        /// <summary>
        /// 切换为当前笔刷时调用
        /// </summary>
        public virtual void OnEnter()
        {
        }

        /// <summary>
        /// 当所选择的Tile发生改变
        /// </summary>
        public virtual void OnTileChanged()
        {
        }

        /// <summary>
        /// 鼠标的网格坐标发生变更，且此时鼠标左键处于按下状态
        /// </summary>
        public virtual void OnMouseGridPosChange()
        {
        }
    }
}