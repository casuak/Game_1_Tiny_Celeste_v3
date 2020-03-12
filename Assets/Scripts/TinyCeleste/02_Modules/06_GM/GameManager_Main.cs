using TinyCeleste._01_Framework;
using UnityEngine;

namespace TinyCeleste._02_Modules._06_GM
{
    public class GameManager_Main : EntitySingleton<GameManager_Main>
    {
        // 玩家重生点
        public Transform playerRebirthPlace;

        protected override void Awake()
        {
            base.Awake();
            Application.targetFrameRate = 60;
        }
    }
}