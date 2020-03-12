using System;
using System.Linq;
using TinyCeleste._01_Framework;
using TinyCeleste._02_Modules._03_Player._01_Action._01_Climb;
using TinyCeleste._02_Modules._07_Physics._04_ColliderChecker;

namespace TinyCeleste._02_Modules._03_Player._02_Other._07_GrabMovePlatform
{
    /// <summary>
    /// 攀爬时抓住移动平台
    /// </summary>
    public class GrabMovePlatform : EntityComponent
    {
        public C_ColliderTag wallCheckerCollider;

        private C_Climb climb;

        private bool hasMovableTag;

        private void Awake()
        {
            climb = GetComponentNotNull<C_Climb>();
            hasMovableTag = wallCheckerCollider.tagList.Any(t => t == E_Tag.Movable);
        }

        public void GrabMovePlatformSystem()
        {
            if (climb.isSlidingOrClimbing && !hasMovableTag)
            {
                hasMovableTag = true;
                wallCheckerCollider.AddTag(E_Tag.Movable);
            }
            else if (!climb.isSlidingOrClimbing && hasMovableTag)
            {
                hasMovableTag = false;
                wallCheckerCollider.RemoveTag(E_Tag.Movable);
            }
        }
    }
}