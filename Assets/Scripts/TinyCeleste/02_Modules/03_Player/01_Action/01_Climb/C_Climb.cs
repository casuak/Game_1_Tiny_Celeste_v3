using System.Collections;
using TinyCeleste._01_Framework;
using TinyCeleste._02_Modules._03_Player._01_Action._04_Face;
using TinyCeleste._02_Modules._03_Player._02_Other._02_Command;
using TinyCeleste._02_Modules._07_Physics._04_ColliderChecker;
using TinyCeleste._02_Modules._08_Proxy;
using UnityEngine;

namespace TinyCeleste._02_Modules._03_Player._01_Action._01_Climb
{
    /// <summary>
    /// 玩家沿墙下滑
    /// 此时玩家的垂直速度会向一个固定的速度靠近
    /// </summary>
    public class C_Climb : EntityComponent
    {
        // 下滑的最大速度
        public float slideDownSpeed = -2.5f;

        // 下滑状态、爬墙状态，两者互斥，不能同时为true
        public bool isSliding;
        public bool isClimbing;
        public bool isSlidingOrClimbing => isClimbing || isSliding;

        // 归0时方可进行攀爬动作
        public float sleepTimer = 0;

        public void SetSleepTime(float sleep = 0.1f)
        {
            sleepTimer = sleep;
            isSliding = false;
            isClimbing = false;
        }

        // 上爬速度
        public float climbSpeed = 5f;

        // 玩家爬出墙的xSpeed（自动着陆）
        public float landXSpeed = 12;

        // 攀爬的最大时长（不向上爬时不计入时间）
        public float maxClimbTime = 2;

        // 攀爬计时器
        public float climbTimer = 0;

        // 从0恢复到攀爬最大时长的时间
        public float resumeTime = 0.5f;

        // 一秒内恢复的m_ClimbTimer
        public float resumeSpeed => maxClimbTime / resumeTime;

        // 剩余的体力条
        public float climbTimeLeftPercent => 1 - climbTimer / maxClimbTime;

        private Face face;
        private Command command;
        private C_Rigidbody2DProxy rigidbody2DWrapper;
        private ColliderCheckerItem wallChecker;
        private ColliderCheckerItem groundChecker;

        private void Awake()
        {
            wallChecker = GetComponentNotNull<C_ColliderChecker>().GetChecker("Wall Checker");
            groundChecker = GetComponentNotNull<C_ColliderChecker>().GetChecker("Ground Checker");

            face = GetComponentNotNull<Face>();
            command = GetComponentNotNull<Command>();
            rigidbody2DWrapper = GetComponentNotNull<C_Rigidbody2DProxy>();
        }

        public void ClimbSystem()
        {
            var deltaTime = Time.deltaTime;
            // 计时器更新
            sleepTimer -= deltaTime;
            if (sleepTimer < 0) sleepTimer = 0;

            if (sleepTimer > 0) return;

            // 满足两个条件，处于依附于墙的状态（下滑、抓墙、上爬）
            // 1. 面前有墙
            // 2. 向墙的方向移动或按下爬墙键
            if (wallChecker.isHit && ((int) face.faceEnum == command.walkInt || command.climbBool))
            {
                // 按下攀爬键并且体力足够
                if (command.climbBool && climbTimer < maxClimbTime)
                {
                    int vInput = command.directionVector2Int.y;
                    rigidbody2DWrapper.SetYSpeedBeforePhysic(climbSpeed * vInput);
                    // 不上下移动时不消耗体力
                    if (vInput != 0)
                        climbTimer += deltaTime;
                    isSliding = false;
                    isClimbing = true;
                }
                // 其余情况处于下滑状态
                else
                {
                    isSliding = true;
                    isClimbing = false;
                    // 垂直速度小于等于最大下滑速度
                    if (rigidbody2DWrapper.velocity.y <= -slideDownSpeed)
                    {
                        rigidbody2DWrapper.SetYSpeedBeforePhysic(slideDownSpeed);
                    }
                }
            }
            // 常态下
            else
            {
                // 脱离上面那个状态
                if (isSliding || isClimbing)
                {
                    isSliding = false;
                    isClimbing = false;
                    StartCoroutine(AutoXSpeed());
//                    rigidbody2DWrapper.SetXSpeedAfterPhysic(landXSpeed * (int) face.faceEnum);
                }
            }
        }

        /// <summary>
        /// 离开墙壁后一段时间设置X轴速度
        /// </summary>
        /// <returns></returns>
        private IEnumerator AutoXSpeed()
        {
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            rigidbody2DWrapper.SetXSpeedAfterPhysic(landXSpeed * (int) face.faceEnum);
        }

        public void ResumeClimbTimeSystem()
        {
            if (groundChecker.isHit)
            {
                climbTimer -= resumeSpeed * Time.deltaTime;
                if (climbTimer < 0) climbTimer = 0;
            }
        }
    }
}