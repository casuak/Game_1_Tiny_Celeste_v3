using TinyCeleste._01_Framework;
using TinyCeleste._02_Modules._08_Proxy;
using UnityEngine;

namespace TinyCeleste._02_Modules._07_Physics._01_Gravity
{
    public class Gravity : EntityComponent
    {
        public GlobalGravity.E_GravityType type = GlobalGravity.E_GravityType.Normal;

        private C_Rigidbody2DProxy rigidbody2DWrapper;

        private void Awake()
        {
            rigidbody2DWrapper = GetComponentNotNull<C_Rigidbody2DProxy>();
        }

        public void GravitySystem()
        {
            var deltaTime = Time.deltaTime;
            var globalGravity = GlobalGravity.Instance;

            // 禁用或者全局重力单例不存在
            if (globalGravity == null) return;
            // 根据重力类型以及当前是否突破最大速度来赋予一个正确的y轴速度
            var gravityParams = globalGravity.GetGravityParams(type);
            float ySpeed = rigidbody2DWrapper.velocity.y - gravityParams.gravity * deltaTime;
            if (gravityParams.hasMaxSpeed)
            {
                if (ySpeed < -gravityParams.maxSpeed)
                {
                    ySpeed = -gravityParams.maxSpeed;
                }
            }

            rigidbody2DWrapper.SetYSpeedBeforePhysic(ySpeed);
        }
    }
}