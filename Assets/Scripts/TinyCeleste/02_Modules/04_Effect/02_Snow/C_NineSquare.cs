using TinyCeleste._01_Framework;
using TinyCeleste._04_Extension._01_UnityComponent;
using TinyCeleste._05_MyTool._06_Math;
using UnityEngine;

namespace TinyCeleste._02_Modules._04_Effect._02_Snow
{
    public class C_NineSquare : EntityComponent
    {
        /// <summary>
        /// 单个方格的大小
        /// </summary>
        public float squareSize;

        /// <summary>
        /// 全局中心位置，代表(0, 0)
        /// </summary>
        public Vector2 globalCenter;

        /// <summary>
        /// 当前中心位置
        /// </summary>
        private Vector2 currentCenter;

        /// <summary>
        /// 追踪对象
        /// </summary>
        public Transform followTarget;

        /// <summary>
        /// 九个下雪特效区块
        /// </summary>
        public Transform[] nineSnowBlocks;

        /// <summary>
        /// 绘制九宫格
        /// </summary>
        public void S_DrawNineSquare()
        {
            var _pos0 = followTarget.position;
            var _center = Vector2.zero;
            var _gridSize = Vector2.one * squareSize;
            var _gridGap = Vector2.zero;
            var center = Tool_Grid.WorldToGridCenter(_pos0, _center, _gridSize, _gridGap);
            for (int y = -1; y <= 1; y++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    _center = center - new Vector2(x, y) * _gridSize;
                    Gizmos.DrawWireCube(_center, _gridSize);
                }
            }
        }

        /// <summary>
        /// 更新九个下雪区块的位置
        /// </summary>
        [ContextMenu("Update Position")]
        public void S_UpdateNineSnowBlock()
        {
            var _pos0 = followTarget.position;
            var _center = Vector2.zero;
            var _gridSize = Vector2.one * squareSize;
            var _gridGap = Vector2.zero;
            var center = Tool_Grid.WorldToGridCenter(_pos0, _center, _gridSize, _gridGap);
            for (int y = -1; y <= 1; y++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    _center = center - new Vector2(x, y) * _gridSize;
                    var _index = (y + 1) * 3 + (x + 1);
                    nineSnowBlocks[_index].SetPos2D(_center);
                }
            }
        }
    }
}