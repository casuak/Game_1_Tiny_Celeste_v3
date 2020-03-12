using TinyCeleste._02_Modules._05_Env._01_Map;
using TinyCeleste._02_Modules._07_Physics._04_ColliderChecker;
using TinyCeleste._02_Modules._08_Proxy;

namespace TinyCeleste._02_Modules._05_Env._03_Spring
{
    public class E_Spring : E_MapElement
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