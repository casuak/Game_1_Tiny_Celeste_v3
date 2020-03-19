using TinyCeleste._01_Framework;
using TinyCeleste._02_Modules._08_Proxy;
using TinyCeleste._04_Extension._01_UnityComponent;
using UnityEngine;

namespace TinyCeleste._02_Modules._05_PrefabTile._04_MovePlatform
{
    /// <summary>
    /// 提供在一连串的路径点之间循环移动的参数和方法
    /// </summary>
    public class C_LoopMove : EntityComponent
    {
        /// <summary>
        /// 平台的Transform(变化)
        /// </summary>
        private C_Transform2DProxy transform2DProxy;

        /// <summary>
        /// 路径点编辑器
        /// </summary>
        public EdgeCollider2D pointsEditor;

        /// <summary>
        /// 路径点数量
        /// </summary>
        private int pointCount => pointsEditor.points.Length;

        /// <summary>
        /// 最近距离（在此距离内视为已经到达下一路径点）
        /// </summary>
        public float minDistance;

        /// <summary>
        /// 移动速度
        /// </summary>
        public float moveSpeed;

        /// <summary>
        /// 当前路径点距离下一路径点的距离
        /// </summary>
        private float currentDistance => (nextPos - transform2DProxy.pos).magnitude;

        /// <summary>
        /// 当前路径点索引
        /// </summary>
        public int lastIndex = 0;

        /// <summary>
        /// 下一路径点索引
        /// </summary>
        private int nextIndex => lastIndex + 1 == pointCount ? 0 : lastIndex + 1;

        /// <summary>
        /// 下一路径点坐标
        /// </summary>
        private Vector2 nextPos => pointsEditor.GetPointPos(nextIndex);

        /// <summary>
        /// 生命周期函数
        /// </summary>
        private void Awake()
        {
            InitDependency();
        }

        /// <summary>
        /// 依赖组件自动赋值
        /// </summary>
        private void InitDependency()
        {
            transform2DProxy = GetComponentNotNull<C_Transform2DProxy>();
        }

        /// <summary>
        /// 提供给外部的循环移动方法
        /// </summary>
        /// <param name="deltaTime">调用方法的时间间隔</param>
        public void S_LoopMove(float deltaTime)
        {
            var dir = (nextPos - transform2DProxy.pos).normalized;
            var moveVec = deltaTime * moveSpeed * dir;
            if (moveVec.magnitude >= currentDistance || currentDistance <= minDistance)
            {
                transform2DProxy.pos = nextPos;
                lastIndex = nextIndex;
            }
            else
            {
                transform2DProxy.Translate(moveVec);
            }
        }
    }
}