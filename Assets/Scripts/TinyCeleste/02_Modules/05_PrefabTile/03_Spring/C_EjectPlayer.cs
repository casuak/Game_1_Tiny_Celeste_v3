using TinyCeleste._01_Framework;
using TinyCeleste._02_Modules._03_Player;
using TinyCeleste._02_Modules._07_Physics._04_ColliderChecker;
using TinyCeleste._02_Modules._08_Proxy;
using UnityEngine;

namespace TinyCeleste._02_Modules._05_PrefabTile._03_Spring
{
    public class C_EjectPlayer : EntityComponent
    {
        private ColliderCheckerItem m_PlayerChecker;
        private C_SpringAnimator m_SpringAnimator;
        private Transform m_Transform;
        
        public float ejectSpeed;

        public bool isEventHappend;

        private void Awake()
        {
            m_PlayerChecker = GetComponentNotNull<C_ColliderChecker>().GetChecker("Player Checker");
            m_SpringAnimator = GetComponentNotNull<C_SpringAnimator>();
            m_Transform = GetComponentNotNull<C_Transform2DProxy>().transform;
        }

        public void EjectPlayerSystem()
        {
            isEventHappend = false;
            // 播放动画时不能弹
            if (m_SpringAnimator.isEjectAnimPlaying) return;
            if (m_PlayerChecker.isHit)
            {
                foreach (var tagContainer in m_PlayerChecker.targetList)
                {
                    var player = (E_Player) tagContainer.GetEntityObject();
                    var rad = (m_Transform.eulerAngles.z + 90) * Mathf.Deg2Rad;
                    var dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
                    Vector2 velocity = ejectSpeed * dir;
                    isEventHappend = player.BeEjected(velocity) || isEventHappend;
                }
            }
        }
    }
}