using System;
using TinyCeleste._01_Framework;
using TinyCeleste._02_Modules._05_Env._01_Map;
using TinyCeleste._02_Modules._07_Physics._04_ColliderChecker;

namespace TinyCeleste._02_Modules._05_Env._02_Balloon
{
    // 当玩家的冲刺次数小于最大冲刺次数时
    // 与气球碰撞后可重新恢复到最大冲刺速度，同时气球摧毁
    // 一般情况下，气球播放飘动的动画
    public class E_Balloon : E_MapElement
    {
        public C_Balloon_ResumeDashCount resumePlayerDashCount;

        public C_ColliderChecker colliderChecker;

        private void Update()
        {
            // 更新碰撞相关的状态变量
            colliderChecker.ColliderCheckerSystem();
            
            // 恢复玩家冲刺次数系统
            resumePlayerDashCount.ResumePlayerDashCountSystem();
        }
    }
}