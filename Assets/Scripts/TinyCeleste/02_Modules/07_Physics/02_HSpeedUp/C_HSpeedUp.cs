using TinyCeleste._01_Framework;
using TinyCeleste._02_Modules._08_Proxy;
using UnityEngine;

namespace TinyCeleste._02_Modules._07_Physics._02_HSpeedUp
{
    public class C_HSpeedUp : EntityComponent
    {
        // > 0 or < 0 (right or left) 目标速度
        public float targetSpeed;

        // > 0 加速度
        public float acceleration;

        private C_Rigidbody2DProxy rigidbody2DWrapper;

        private void Awake()
        {
            rigidbody2DWrapper = GetComponentNotNull<C_Rigidbody2DProxy>();
        }

        public void HSpeedUpSystem()
        {
            float deltaTime = Time.deltaTime;
            
            float xSpeed = rigidbody2DWrapper.velocity.x;
            float deltaSpeed = deltaTime * acceleration;

            if (xSpeed < targetSpeed)
            {
                xSpeed += deltaSpeed;
                if (xSpeed > targetSpeed)
                    xSpeed = targetSpeed;
            }
            else if (xSpeed > targetSpeed)
            {
                xSpeed -= deltaSpeed;
                if (xSpeed < targetSpeed)
                    xSpeed = targetSpeed;
            }

            rigidbody2DWrapper.SetXSpeedBeforePhysic(xSpeed);
        }
    }
}