using TinyCeleste._01_Framework;
using TinyCeleste._02_Modules._08_Proxy;
using UnityEngine;

namespace TinyCeleste._02_Modules._02_Camera
{
    public class C_CameraFollow : EntityComponent
    {
        /// <summary>
        /// 追踪的目标
        /// </summary>
        public Transform followTarget;

        /// <summary>
        /// 每帧逼近剩余距离的百分比
        /// </summary>
        public float interpolate = 0.05f;

        /// <summary>
        /// 是否禁用跟随效果
        /// </summary>
        public bool disable;

        /// <summary>
        /// 可以保持的最大距离，在此距离下，视物体已经到达目标位置
        /// </summary>
        public float maxDistance = 0.1f;
        public float maxDistance2 => maxDistance * maxDistance;

        // 最小移动距离
        /// <summary>
        /// 最小移动距离，若小于此距离，则移动这个最小距离的长度
        /// </summary>
        public float minMoveDistance;

        /// <summary>
        /// transform代理
        /// </summary>
        private C_Transform2DProxy transform2DProxy;

        /// <summary>
        /// 刚体代理
        /// </summary>
        private C_Rigidbody2DProxy rigidbody2DProxy;

        private void Awake()
        {
            transform2DProxy = GetComponentNotNull<C_Transform2DProxy>();
            rigidbody2DProxy = GetComponentNotNull<C_Rigidbody2DProxy>();
        }

        public void FollowSystem(float deltaTime)
        {
            var transform = transform2DProxy.transform;
            if (disable) return;
            Vector2 currentPos = transform.position;
            Vector2 distance = (Vector2) followTarget.position - currentPos;
            // 若小于最大距离，则不需要再继续移动
            if (distance.sqrMagnitude < maxDistance2) return;
            // 计算下一目标位置
            Vector2 nextPos = currentPos + distance * interpolate;
            // 到下一位置移动距离的平方
            var moveDistance2 = (nextPos - currentPos).sqrMagnitude;
            // 最小移动距离的平方
            var minMoveDisntace2 = minMoveDistance * minMoveDistance;
            // 移动距离小于最小移动距离
            var velocity = (nextPos - currentPos) / deltaTime;
            if (moveDistance2 < minMoveDisntace2)
            {
                // 总距离也小于最小移动距离，则直接设置位置
                if (distance.sqrMagnitude < minMoveDisntace2)
                {
                    transform.position = followTarget.position;
                    velocity = Vector2.zero;
                }
                // 设置下一位置为当前位置加最小位移
                else
                {
                    nextPos = currentPos + distance.normalized * minMoveDistance;
                    velocity = (nextPos - currentPos) / deltaTime;
                }
            }
            rigidbody2DProxy.SetVelocityBeforePhysic(velocity);
        }
    }
}