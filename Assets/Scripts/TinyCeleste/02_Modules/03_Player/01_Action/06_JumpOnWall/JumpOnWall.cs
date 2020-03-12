using TinyCeleste._01_Framework;
using TinyCeleste._02_Modules._03_Player._01_Action._01_Climb;
using TinyCeleste._02_Modules._03_Player._01_Action._04_Face;
using TinyCeleste._02_Modules._03_Player._02_Other._02_Command;
using TinyCeleste._02_Modules._07_Physics._04_ColliderChecker;
using TinyCeleste._02_Modules._08_Proxy;
using UnityEngine;

namespace TinyCeleste._02_Modules._03_Player._01_Action._06_JumpOnWall
{
    /// <summary>
    /// 蹬墙跳
    /// 跳的方向：
    /// y轴向上
    /// x轴取决于face的反方向
    /// </summary>
    public class JumpOnWall : EntityComponent
    {
        // 起跳速率
        public float jumpSpeed = 18;

        // 起跳方向
        public Vector2 baseDirection = new Vector2(1, 1);

        private Command command;
        private Face face;
        private C_Rigidbody2DProxy rigidbody2DWrapper;
        private C_Climb climb;
        private ColliderCheckerItem groundChecker;

        private void Awake()
        {
            command = GetComponentNotNull<Command>();
            face = GetComponentNotNull<Face>();
            rigidbody2DWrapper = GetComponentNotNull<C_Rigidbody2DProxy>();
            climb = GetComponentNotNull<C_Climb>();
            groundChecker = GetComponentNotNull<C_ColliderChecker>().GetChecker("Ground Checker");
        }

        public void JumpOnWallSystem()
        {
            if (!command.jumpBool) return;
            // 攀爬或下滑
            if (!climb.isSliding && !climb.isClimbing) return;

            Vector2 direction = new Vector2(-(int) face.faceEnum * baseDirection.x, baseDirection.y);
            if (groundChecker.isHit) direction.x = 0;
            Vector2 velocity = direction * jumpSpeed;
            rigidbody2DWrapper.SetVelocityBeforePhysic(velocity);
            climb.SetSleepTime(0.1f);
            if (!groundChecker.isHit)
                face.Switch();
        }
    }
}