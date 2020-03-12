using System.Collections.Generic;
using TinyCeleste._01_Framework;
using TinyCeleste._02_Modules._03_Player;
using TinyCeleste._02_Modules._04_Effect._01_Dust;
using TinyCeleste._02_Modules._07_Physics._04_ColliderChecker;
using UnityEngine;

namespace TinyCeleste._02_Modules._05_Env._06_DestructiveBlock
{
    public class C_BeDestroy : EntityComponent
    {
        public List<Transform> blockList;

        private ColliderCheckerItem playerChecker;

        private void Awake()
        {
            playerChecker = GetComponentNotNull<C_ColliderChecker>().GetChecker("Player Checker");
        }

        public void BeDestroySystem()
        {
            if (!playerChecker.isHit) return;
            var player = playerChecker.firstTarget.GetEntityObject() as E_Player;
            // 检测到玩家并且玩家正处于冲刺中
            if (player != null && player.dashing.isDashing && player.dashing.isAdvancedEnd)
            {
                // 销毁自身
                Destroy(gameObject, 0.05f);
                // 产生摧毁特效
                DestroyEffect();
            }
        }

        public void DestroyEffect()
        {
            // 产生多个灰尘特效
            foreach (var t in blockList)
            {
                var dust = S_Dust_Factory.Instance.CreateDust(t.position);
                dust.SetAnimatorSpeed(1.2f);
            }
        }
    }
}