using TinyCeleste._02_Modules._05_Env._01_Map;
using TinyCeleste._02_Modules._07_Physics._04_ColliderChecker;

namespace TinyCeleste._02_Modules._05_Env._06_DestructiveBlock
{
    // 玩家处于冲刺状态下与该类Block碰撞时（理想情况下，仅当）
    public class E_DestructiveBlock : E_MapElement
    {
        public C_BeDestroy beDestroy;
        public C_ColliderChecker colliderChecker;

        private void Update()
        {
            colliderChecker.ColliderCheckerSystem();
            beDestroy.BeDestroySystem();
        }
    }
}