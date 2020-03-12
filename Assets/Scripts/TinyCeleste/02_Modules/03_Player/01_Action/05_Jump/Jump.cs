using TinyCeleste._01_Framework;
using TinyCeleste._02_Modules._03_Player._02_Other._02_Command;
using TinyCeleste._02_Modules._07_Physics._04_ColliderChecker;
using TinyCeleste._02_Modules._08_Proxy;
using UnityEngine;

namespace TinyCeleste._02_Modules._03_Player._01_Action._05_Jump
{
    public class Jump : EntityComponent
    {
        // 起跳速度
        public float jumpSpeed = 20;

        // 跳跃事件
        public bool jumpEvent;

        private Command command;
        private ColliderCheckerItem groundChecker;
        private ColliderCheckerItem platformChecker;
        private C_Rigidbody2DProxy rigidbody2DWrapper;

        private void Awake()
        {
            command = GetComponentNotNull<Command>();
            groundChecker = GetComponentNotNull<C_ColliderChecker>().GetChecker("Ground Checker");
            platformChecker = GetComponentNotNull<C_ColliderChecker>().GetChecker("Platform Checker");
            rigidbody2DWrapper = GetComponentNotNull<C_Rigidbody2DProxy>();
        }

        public void JumpSystem()
        {
            jumpEvent = false;
            if (platformChecker.isHit && command.directionVector2Int.y < 0) return;
            if (command.jumpBool && groundChecker.isHit)
            {
                rigidbody2DWrapper.SetYSpeedBeforePhysic(jumpSpeed);
                jumpEvent = true;
            }
        }
    }
}