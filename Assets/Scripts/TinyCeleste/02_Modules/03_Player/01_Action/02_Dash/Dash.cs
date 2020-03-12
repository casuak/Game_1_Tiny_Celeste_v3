using TinyCeleste._01_Framework;
using TinyCeleste._02_Modules._02_Camera;
using TinyCeleste._02_Modules._03_Player._01_Action._01_Climb;
using TinyCeleste._02_Modules._03_Player._01_Action._04_Face;
using TinyCeleste._02_Modules._03_Player._02_Other._02_Command;
using TinyCeleste._02_Modules._08_Proxy;
using UnityEngine;

namespace TinyCeleste._02_Modules._03_Player._01_Action._02_Dash
{
    /// <summary>
    /// 特殊动作：冲刺
    /// 冲刺时，人物的将
    ///     1：不受Gravity影响
    ///     2：无法Jump
    ///     3：无法Walk
    ///     4：无法Dash
    /// </summary>
    public class Dash : EntityComponent
    {
        // 冲刺速率
        public float dashSpeed = 30;

        // 冲刺持续时间
        public float duration = 0.2f;

        // 创建特效的时间间隔
        public float effectInterval = 0.04f;

        // 创建特效的相对位置
        public Vector2 effectPosition = Vector2.zero;

        private Dashing dashing;
        private DashCount dashCount;
        private Command command;
        private Face face;
        private C_Rigidbody2DProxy rigidbody2DWrapper;
        private C_Climb climb;
        
        private void Awake()
        {
            dashing = GetComponentNotNull<Dashing>();
            dashCount = GetComponentNotNull<DashCount>();
            command = GetComponentNotNull<Command>();
            face = GetComponentNotNull<Face>();
            rigidbody2DWrapper = GetComponentNotNull<C_Rigidbody2DProxy>();
            climb = GetComponentNotNull<C_Climb>();
        }

        public void TriggerDashSystem()
        {
            // 没有接受到dash指令
            if (!command.dashBool) return;
            // 可用的dash次数不够
            if (dashCount.count <= 0) return;
            // 当前正处于dash状态
            if (dashing.isDashing) return;

            // 触发相机抖动特效
            S_MainCamera.Instance.Shake(C_CameraShake.ShakeType.Dash);
            // 触发dash
            dashing.isDashing = true;
            dashCount.count -= 1;
            // 初始化dashing组件
            dashing.endTimer = 0;
            dashing.effectTimer = effectInterval;
            dashing.effectColor = dashCount.countToColor[dashCount.count];
            dashing.isAdvancedEnd = false;
            // 给刚体组件的速度赋值
            Vector2 direction = command.directionVector2Int;
            if (direction.Equals(Vector2Int.zero))
                direction = Vector2Int.right * (int) face.faceEnum;
            rigidbody2DWrapper.SetVelocityBeforePhysic(direction.normalized * dashSpeed);
            climb.SetSleepTime();
        }
    }
}