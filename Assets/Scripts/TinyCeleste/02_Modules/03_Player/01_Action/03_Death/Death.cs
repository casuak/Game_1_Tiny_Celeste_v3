using TinyCeleste._01_Framework;
using TinyCeleste._02_Modules._02_Camera;
using TinyCeleste._02_Modules._03_Player._02_Other._02_Command;
using TinyCeleste._02_Modules._03_Player._02_Other._05_Pause;
using TinyCeleste._02_Modules._03_Player._02_Other._06_BugColliderChecker;
using TinyCeleste._02_Modules._04_Effect._01_Dust;
using TinyCeleste._02_Modules._04_Effect._01_Dust._01_Event;
using TinyCeleste._02_Modules._06_GM;
using TinyCeleste._02_Modules._07_Physics._04_ColliderChecker;
using TinyCeleste._02_Modules._08_Proxy;
using UnityEngine;

namespace TinyCeleste._02_Modules._03_Player._01_Action._03_Death
{
    public class Death : EntityComponent, I_OnDustDestroy
    {
        // 是否处于死亡状态
        public bool isDead;

        // 死亡动画是否完成
        public bool isDeathAnimFinish;

        private Command command;
        private PlayerPause playerPause;
        private new Transform transform;
        private E_Player ePlayer;
        private ColliderCheckerItem sharpPointChecker;
        private C_Rigidbody2DProxy m_Rigidbody2DWrapper;
        private BugColliderChecker bugColliderChecker;

        private void Awake()
        {
            command = GetComponentNotNull<Command>();
            playerPause = GetComponentNotNull<PlayerPause>();
            transform = GetComponentNotNull<C_Transform2DProxy>().transform;
            ePlayer = GetComponentNotNull<E_Player>();
            sharpPointChecker = GetComponentNotNull<C_ColliderChecker>().GetChecker("Sharp Point Checker");
            m_Rigidbody2DWrapper = GetComponentNotNull<C_Rigidbody2DProxy>();
            bugColliderChecker = GetComponentNotNull<BugColliderChecker>();
        }

        public void DeathSystem()
        {
            bool c1 = command.deathBool;
            bool c2 = sharpPointChecker.isHit;
            bool c3 = bugColliderChecker.isHit;
            if (c1 || c2 || c3)
            {
                Die();
            }
        }

        // 死亡
        public void Die()
        {
            if (isDead) return;
            m_Rigidbody2DWrapper.Pause(false);
            isDead = true;
            // 隐藏Player
            playerPause.Hide();
            // 产生Death动画预制体（播放完后自动销毁）
            var dust = S_Dust_Factory.Instance.CreateDust(transform.position);
            dust.AddObserver(this);
            S_MainCamera.Instance.Shake(C_CameraShake.ShakeType.Die);
        }

        // 监听死亡动画完成事件
        // 仅两种情况：播放完成死亡动画、播放完成重生动画
        // 完成重生动画的事件必然发生在死亡动画播放完成
        public void OnDustDestroy()
        {
            if (!isDeathAnimFinish)
            {
                OnDeathAnimFinish();
            }
            else
            {
                OnRebirthAnimFinish();
            }
        }

        public void OnDeathAnimFinish()
        {
            // 重新设置Player的位置
            ePlayer.SetPosition(
                GameManager_Main.Instance.playerRebirthPlace.position);
            m_Rigidbody2DWrapper.Resume();
            // 产生Rebirth动画预制体（播放完成后自动销毁）
            var dust = S_Dust_Factory.Instance.CreateDust(transform.position);
            dust.AddObserver(this);
            isDeathAnimFinish = true;
        }

        public void OnRebirthAnimFinish()
        {
            // 显示Player
            playerPause.Show();
            // 重设死亡状态
            isDead = false;
            isDeathAnimFinish = false;
        }
    }
}