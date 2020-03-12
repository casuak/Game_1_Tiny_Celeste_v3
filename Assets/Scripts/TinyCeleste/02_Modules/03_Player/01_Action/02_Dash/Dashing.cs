using TinyCeleste._01_Framework;
using TinyCeleste._02_Modules._04_Effect._01_Dust;
using TinyCeleste._02_Modules._08_Proxy;
using UnityEngine;

namespace TinyCeleste._02_Modules._03_Player._01_Action._02_Dash
{
    public class Dashing : EntityComponent
    {
        // 处于冲刺状态
        public bool isDashing;

        // 生成特效的颜色
        public Color effectColor;

        // 下一个特效生成的时间
        public float effectTimer;

        // 结束冲刺状态的计时器
        public float endTimer;

        // 非正常结束(提前结束冲刺)
        public bool isAdvancedEnd;
        
        // 尘土特效的流动速度
        public float dustFlowSpeed = 0.1f;

        private Dash dash;
        private C_Rigidbody2DProxy rigidbody2DWrapper;
        private new Transform transform;

        private void Awake()
        {
            dash = GetComponentNotNull<Dash>();
            rigidbody2DWrapper = GetComponentNotNull<C_Rigidbody2DProxy>();
            transform = GetComponentNotNull<C_Transform2DProxy>().transform;
        }

        public void DashingSystem()
        {
            var deltaTime = Time.deltaTime;

            // 间隔时间创建特效
            if (effectTimer >= dash.effectInterval)
            {
                effectTimer = 0;
                var createPosition = dash.effectPosition + (Vector2) transform.position;
                var flowDirection = -rigidbody2DWrapper.velocity.normalized;
                S_Dust_Factory.Instance.CreateDust(createPosition, flowDirection, effectColor)
                    .SetLightActive(true)
                    .SetLightColor(effectColor)
                    .SetFlowSpeed(dustFlowSpeed);
            }
            else
            {
                effectTimer += deltaTime;
            }

            var velocity = rigidbody2DWrapper.velocity;
            // 冲刺时间结束或遇到障碍物时速度归零(unity的2D物理引擎控制)
            if (endTimer >= dash.duration)
            {
                isAdvancedEnd = false;
                rigidbody2DWrapper.SetVelocityBeforePhysic(Vector2.zero);
                isDashing = false;
            }
            else if (velocity.sqrMagnitude < dash.dashSpeed * dash.dashSpeed * 0.5f && !isAdvancedEnd)
            {
                AdvanceEnd();
            }
            else
            {
                endTimer += deltaTime;
            }
        }

        public void AdvanceEnd(float lastSecond = 0.05f)
        {
            isAdvancedEnd = true;
            endTimer = dash.duration - lastSecond;
        }
    }
}