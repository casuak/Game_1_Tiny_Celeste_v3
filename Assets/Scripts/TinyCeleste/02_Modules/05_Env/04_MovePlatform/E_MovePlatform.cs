using TinyCeleste._02_Modules._05_Env._01_Map;
using TinyCeleste._02_Modules._07_Physics._04_ColliderChecker;
using TinyCeleste._02_Modules._08_Proxy;
using UnityEngine;

namespace TinyCeleste._02_Modules._05_Env._04_MovePlatform
{
    public class E_MovePlatform : E_MapElement
    {
        public C_Transform2DProxy transform2DProxy;
        public C_LoopMove loopMove;
        public C_GrabOrReleaseChild grabOrReleaseChild;
        public C_ColliderChecker colliderChecker;
        
        private void Update()
        {
            colliderChecker.ColliderCheckerSystem();
            grabOrReleaseChild.TakeInChildrenSystem();
        }
        
        private void FixedUpdate()
        {
            loopMove.S_LoopMove(Time.fixedDeltaTime);
        }
    }
}