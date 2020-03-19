using TinyCeleste._01_Framework;
using TinyCeleste._02_Modules._08_Proxy;
using UnityEngine;

namespace TinyCeleste._02_Modules._05_PrefabTile._03_Spring
{
    public class C_SpringAnimator : EntityComponent
    {
        private C_AnimatorProxy cAnimatorProxy;
        private C_EjectPlayer m_EjectPlayer;
        private C_AutoSpringType autoSpringType;

        private void Awake()
        {
            cAnimatorProxy = GetComponentNotNull<C_AnimatorProxy>();
            m_EjectPlayer = GetComponentNotNull<C_EjectPlayer>();
            autoSpringType = GetComponentNotNull<C_AutoSpringType>();
        }

        private static readonly int String_Clip = Animator.StringToHash("Clip");

        private enum ClipEnum
        {
            Idle = 1,
            Eject = 2,
            Eject2 = 3,
        }

        public void SpringAnimatorSystem()
        {
            ClipEnum clipEnum = currentClip;
            if (m_EjectPlayer.isEventHappend)
            {
                if (autoSpringType.springType == C_AutoSpringType.Enum_SpringType.Out)
                    clipEnum = ClipEnum.Eject;
                else if (autoSpringType.springType == C_AutoSpringType.Enum_SpringType.In) 
                    clipEnum = ClipEnum.Eject2;
            }
            else if (cAnimatorProxy.isFinished)
                clipEnum = ClipEnum.Idle;

            cAnimatorProxy.animator.SetInteger(String_Clip, (int) clipEnum);
        }

        private ClipEnum currentClip => (ClipEnum) cAnimatorProxy.animator.GetInteger(String_Clip);

        public bool isEjectAnimPlaying => currentClip == ClipEnum.Eject;
    }
}