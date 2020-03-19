using TinyCeleste._02_Modules._07_Physics._04_ColliderChecker;
using TinyCeleste._02_Modules._08_Proxy;
using TinyCeleste._06_Plugins._01_PrefabTileMap;

namespace TinyCeleste._02_Modules._05_PrefabTile._03_Spring
{
    public class E_Spring : E_PrefabTile
    {
        public C_AnimatorProxy animatorProxy;
        
        public C_EjectPlayer ejectPlayer;
        public C_ColliderChecker colliderChecker;
        public C_SpringAnimator springAnimator;
        public C_AutoSpringType autoSpringType;

        private void Update()
        {
            colliderChecker.ColliderCheckerSystem();
            ejectPlayer.EjectPlayerSystem();
            springAnimator.SpringAnimatorSystem();
        }
    }
}