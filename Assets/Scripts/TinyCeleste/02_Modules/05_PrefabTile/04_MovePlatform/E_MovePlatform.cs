using TinyCeleste._02_Modules._07_Physics._04_ColliderChecker;
using TinyCeleste._02_Modules._08_Proxy;
using TinyCeleste._06_Plugins._01_PrefabTileMap;
using UnityEngine;

namespace TinyCeleste._02_Modules._05_PrefabTile._04_MovePlatform
{
    public class E_MovePlatform : E_PrefabTile
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