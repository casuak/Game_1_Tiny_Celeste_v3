using Casuak.MyTool._02_Game;
using TinyCeleste._01_Framework;
using UnityEngine;

namespace TinyCeleste._02_Modules._03_Player._02_Other._02_Command
{
    public class Command : EntityComponent
    {
        public bool isPaused;

        // 行走
        public int walkInt;

        // 跳跃
        public bool jumpBool;

        // 方向键（决定冲刺的方向）
        public Vector2Int directionVector2Int;

        // 冲刺
        public bool dashBool;

        // 攀爬
        public bool climbBool;
        
        // 死亡
        public bool deathBool;

        public void CommandSystem()
        {
            // 暂停状态下，需要提供一个默认的输入
            if (isPaused)
            {
                jumpBool = false;
                walkInt = 0;
                directionVector2Int = Vector2Int.zero;
                dashBool = false;
                climbBool = false;
                deathBool = false;
            }
            else
            {
                jumpBool = Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.Joystick1Button0);
                walkInt = Tool_Input.GetHorizontalInput();
                directionVector2Int = Tool_Input.GetHVInput();
                dashBool = Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Joystick1Button1);
                climbBool = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Joystick1Button4);
                deathBool = Input.GetKeyDown(KeyCode.Return);
            }
        }
    }
}