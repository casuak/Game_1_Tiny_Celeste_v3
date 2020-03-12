using TinyCeleste._01_Framework;
using TinyCeleste._02_Modules._03_Player;
using TinyCeleste._02_Modules._04_Effect._01_Dust;
using TinyCeleste._02_Modules._07_Physics._04_ColliderChecker;

namespace TinyCeleste._02_Modules._05_Env._02_Balloon
{
    public class C_Balloon_ResumeDashCount : EntityComponent
    {
        private ColliderCheckerItem playerChecker;

        private void Awake()
        {
            playerChecker = GetComponentNotNull<C_ColliderChecker>().GetChecker("Player Checker");
        }

        public void ResumePlayerDashCountSystem()
        {
            if (playerChecker.isHit)
            {
                bool isEventHappend = false;
                foreach (var tagContainer in playerChecker.targetList)
                {
                    var player = (E_Player) tagContainer.GetEntityObject();
                    isEventHappend = player.ResumeDashCount() || isEventHappend;
                }

                if (isEventHappend) Boom();
            }
        }

        private void Boom()
        {
            Destroy(gameObject);
            S_Dust_Factory.Instance.CreateDust(transform.position);
        }
    }
}