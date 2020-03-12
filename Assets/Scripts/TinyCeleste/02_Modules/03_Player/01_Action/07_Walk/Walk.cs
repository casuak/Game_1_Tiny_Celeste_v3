using TinyCeleste._01_Framework;
using TinyCeleste._02_Modules._03_Player._02_Other._02_Command;
using TinyCeleste._02_Modules._07_Physics._02_HSpeedUp;

namespace TinyCeleste._02_Modules._03_Player._01_Action._07_Walk
{
    public class Walk : EntityComponent
    {
        // 行走的最大速度
        public float maxSpeed = 8;

        // 加速度
        public float acceleration = 80;

        // 减速度
        public float deceleration = 80;

        private HSpeedUp hSpeedUp;
        private Command command;

        private void Awake()
        {
            hSpeedUp = GetComponentNotNull<HSpeedUp>();
            command = GetComponentNotNull<Command>();
        }

        public void WalkSystem()
        {
            int walkInt = command.walkInt;
            if (walkInt != 0)
            {
                hSpeedUp.targetSpeed = maxSpeed * walkInt;
                hSpeedUp.acceleration = acceleration;
            }
            else
            {
                hSpeedUp.targetSpeed = 0;
                hSpeedUp.acceleration = deceleration;
            }
        }
    }
}