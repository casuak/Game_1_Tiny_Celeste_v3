using TinyCeleste._01_Framework;
using TinyCeleste._02_Modules._03_Player._01_Action._01_Climb;
using TinyCeleste._02_Modules._03_Player._01_Action._02_Dash;
using TinyCeleste._02_Modules._03_Player._02_Other._04_Hair;
using TinyCeleste._02_Modules._07_Physics._04_ColliderChecker;
using TinyCeleste._02_Modules._08_Proxy;
using UnityEngine;

namespace TinyCeleste._02_Modules._03_Player._02_Other._01_Animator
{
    public class PlayerAnimator : EntityComponent
    {
        private C_Rigidbody2DProxy rigidbody2DWrapper;
        private ColliderCheckerItem groundChecker;
        private C_Climb climb;
        private C_AnimatorProxy animatorProxy;
        private Dashing dashing;
        private HairFlow hairFlow;

        private void Awake()
        {
            rigidbody2DWrapper = GetComponentNotNull<C_Rigidbody2DProxy>();
            groundChecker = GetComponentNotNull<C_ColliderChecker>().GetChecker("Ground Checker");
            climb = GetComponentNotNull<C_Climb>();
            animatorProxy = GetComponentNotNull<C_AnimatorProxy>();
            dashing = GetComponentNotNull<Dashing>();
            hairFlow = GetComponentNotNull<HairFlow>();
        }

        private static readonly int String_Clip = Animator.StringToHash("Clip");

        private enum ClipEnum
        {
            Idle = 1,
            Run = 2,
            Jump = 3,
            Slide = 4,
            Climb = 5,
            Idle2 = 6,
        }

        public void PlayerAnimatorSystem()
        {
            ClipEnum clipEnum = ClipEnum.Idle;

            if (climb.isSliding)
                clipEnum = ClipEnum.Slide;
            else if (climb.isClimbing)
                clipEnum = Mathf.Abs(rigidbody2DWrapper.velocity.y) > 0.01f ? ClipEnum.Climb : ClipEnum.Slide;
            else if (!groundChecker.isHit || dashing.isDashing)
                clipEnum = ClipEnum.Jump;
            else if (Mathf.Abs(rigidbody2DWrapper.velocity.x) > 0.1f)
                clipEnum = ClipEnum.Run;
            else if (hairFlow.isPlayerMoved)
                clipEnum = ClipEnum.Idle2;

            animatorProxy.SetInteger(String_Clip, (int) clipEnum);
        }
    }
}