using System;
using TinyCeleste._01_Framework;
using TinyCeleste._02_Modules._03_Player._01_Action._01_Climb;
using TinyCeleste._02_Modules._03_Player._01_Action._02_Dash;
using TinyCeleste._02_Modules._03_Player._01_Action._03_Death;
using TinyCeleste._02_Modules._03_Player._01_Action._04_Face;
using TinyCeleste._02_Modules._03_Player._01_Action._05_Jump;
using TinyCeleste._02_Modules._03_Player._01_Action._06_JumpOnWall;
using TinyCeleste._02_Modules._03_Player._01_Action._07_Walk;
using TinyCeleste._02_Modules._03_Player._01_Action._08_PlatformCollider;
using TinyCeleste._02_Modules._03_Player._02_Other._01_Animator;
using TinyCeleste._02_Modules._03_Player._02_Other._02_Command;
using TinyCeleste._02_Modules._03_Player._02_Other._03_FootDust;
using TinyCeleste._02_Modules._03_Player._02_Other._04_Hair;
using TinyCeleste._02_Modules._03_Player._02_Other._05_Pause;
using TinyCeleste._02_Modules._03_Player._02_Other._06_BugColliderChecker;
using TinyCeleste._02_Modules._03_Player._02_Other._07_GrabMovePlatform;
using TinyCeleste._02_Modules._07_Physics._01_Gravity;
using TinyCeleste._02_Modules._07_Physics._02_HSpeedUp;
using TinyCeleste._02_Modules._07_Physics._04_ColliderChecker;
using TinyCeleste._02_Modules._08_Proxy;
using UnityEngine;

namespace TinyCeleste._02_Modules._03_Player
{
    public class E_Player : Entity
    {
        // wrapper类通用组件
        public C_Rigidbody2DProxy rigidbody2DWrapper;
        public C_AnimatorProxy animatorProxy;
        public C_Transform2DProxy transform2DProxy;

        // physics类通用组件
        public C_HSpeedUp hSpeedUp;
        public C_Gravity gravity;

        // action
        public C_Climb climb;
        public C_ClimbEffect climbEffect;
        public C_ClimbTimeBar climbTimeBar;

        public Dash dash;
        public DashCount dashCount;
        public Dashing dashing;

        public Death death;
        public Face face;
        public Jump jump;
        public JumpOnWall jumpOnWall;
        public Walk walk;

        // assist
        public Command command;
        public C_ColliderChecker colliderChecker;

        // view
        public PlayerAnimator playerAnimator;
        public FootDust footDust;
        public HairFlow hairFlow;
        public HairSprite hairSprite;
        public PlayerPause playerPause;

        public PlatformCollider platformCollider;

        public BugColliderChecker bugColliderChecker;

        private ColliderCheckerItem groundChecker;

        public GrabMovePlatform grabMovePlatform;

        private void Awake()
        {
            groundChecker = colliderChecker.GetChecker("Ground Checker");
        }

        // 瞬间移动（非物理移动）
        // 物理移动应去改变Rigidbody的velocity
        public void SetPosition(Vector2 position)
        {
            transform2DProxy.pos = position;
            hairFlow.ResetPlace();
        }

        // 完全恢复冲刺速度
        public bool ResumeDashCount()
        {
            return dashCount.ResumeDashCount();
        }

        // 被直接赋予某个方向上的速度
        public bool BeEjected(Vector2 velocity)
        {
            rigidbody2DWrapper.SetVelocityAfterPhysic(velocity);
            dashing.AdvanceEnd(0);
            return true;
        }

        private void Update()
        {
            // 玩家输入指令更新
            command.CommandSystem();


            if (playerPause.isPaused) return;
            // 更新碰撞相关的状态变量
            colliderChecker.ColliderCheckerSystem();
            // 更新卡bug探测器
            bugColliderChecker.BugColliderCheckerSystem();
            // 跳跃（不在冲刺、攀墙状态时可进行），且方向键无向下输入
            if (!climb.isSlidingOrClimbing && !dashing.isDashing)
                jump.JumpSystem();
            // 行走（不在冲刺、攀墙状态时可进行）
            if (!climb.isSlidingOrClimbing && !dashing.isDashing)
                walk.WalkSystem();
            // 更新玩家朝向（不在冲刺、攀墙状态下可进行）
            if (!climb.isSlidingOrClimbing && !dashing.isDashing)
                face.FaceSystem();

            // 蹬墙跳
            jumpOnWall.JumpOnWallSystem();
            // 攀爬（包括下滑）
            if (!dashing.isDashing)
                climb.ClimbSystem();
            // 恢复攀爬时间
            climb.ResumeClimbTimeSystem();
            // 攀爬特效（在下滑状态下可进行）
            if (climb.isSliding)
                climbEffect.ClimbEffectSystem();
            // 攀爬条的更新
            climbTimeBar.ClimbTimeBarSystem();

            // 触发冲刺
            dash.TriggerDashSystem();
            // 冲刺进行时
            if (dashing.isDashing)
                dashing.DashingSystem();
            // 恢复可冲刺次数（不在冲刺状态下可进行）
            if (!dashing.isDashing)
                dashCount.ResumeDashCountSystem();

            // 玩家动画更新
            playerAnimator.PlayerAnimatorSystem();
            // 脚底灰尘特效
            if (!climb.isClimbing)
                footDust.FootDustSystem();
            // 更新玩家头发颜色
            hairFlow.HairColorSystem();
            // 重力特效（不在爬墙、冲刺状态下可进行）
            if (!climb.isClimbing && !dashing.isDashing)
                gravity.GravitySystem();
            // 水平速度控制（不在冲刺状态下可进行）
            if (!dashing.isDashing)
                hSpeedUp.HSpeedUpSystem();
            // 触发玩家死亡系统
            death.DeathSystem();

            // 抓取系统
            grabMovePlatform.GrabMovePlatformSystem();
            
            platformCollider.PlatformColliderSystem();
        }

        private void FixedUpdate()
        {
            hairFlow.HairFlowSystem();
        }
    }
}