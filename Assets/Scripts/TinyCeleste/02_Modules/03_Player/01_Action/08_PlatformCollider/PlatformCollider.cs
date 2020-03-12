using TinyCeleste._01_Framework;
using TinyCeleste._02_Modules._03_Player._01_Action._01_Climb;
using TinyCeleste._02_Modules._03_Player._02_Other._02_Command;
using TinyCeleste._02_Modules._07_Physics._04_ColliderChecker;
using TinyCeleste._02_Modules._08_Proxy;
using UnityEngine;

namespace TinyCeleste._02_Modules._03_Player._01_Action._08_PlatformCollider
{
    public class PlatformCollider : EntityComponent
    {
        public new Collider2D collider;
        public float dropTime = 0.2f;
        // 下落时的初速度
        public float dropDownSpeed;

        private C_Rigidbody2DProxy m_Rigidbody2DWrapper;
        private Command m_Command;
        private C_Climb m_Climb;
        private ColliderCheckerItem platformChecker;
        private float m_Timer;

        private void Awake()
        {
            m_Rigidbody2DWrapper = GetComponentNotNull<C_Rigidbody2DProxy>();
            m_Command = GetComponentNotNull<Command>();
            m_Climb = GetComponentNotNull<C_Climb>();
            platformChecker = GetComponentNotNull<C_ColliderChecker>().GetChecker("Platform Checker");
        }

        public void PlatformColliderSystem()
        {
            // 向上运动或按下方向键下+跳跃键时，进入一段时间的下落
            var velocity = m_Rigidbody2DWrapper.velocity;
            bool condition1 = velocity.y > Mathf.Epsilon;
            bool condition2 = m_Command.jumpBool && m_Command.directionVector2Int.y < 0;
            bool condition3 = m_Timer > Mathf.Epsilon;
            bool condition4 = m_Climb.isClimbing;
            collider.isTrigger = condition1 || condition2 || condition3 || condition4;

            if (condition2 && platformChecker.isHit)
            {
                m_Timer = dropTime;
                m_Rigidbody2DWrapper.SetYSpeedAfterPhysic(dropDownSpeed);
            }
            
            m_Timer -= Time.deltaTime;
            if (m_Timer < 0) m_Timer = 0;
        }
    }
}