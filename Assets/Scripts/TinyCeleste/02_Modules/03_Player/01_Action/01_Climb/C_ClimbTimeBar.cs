using TinyCeleste._01_Framework;
using UnityEngine;
using UnityEngine.UI;

namespace TinyCeleste._02_Modules._03_Player._01_Action._01_Climb
{
    public class C_ClimbTimeBar : EntityComponent
    {
        // 前景
        public Image foreground;

        // 背景
        public Image background;
        
        // 记录下隐藏前的Alpha值
        public float foregroundAlpha;
        public float backgroundAlpha;

        // 显隐的过渡时间
        public float switchTime = 0.33f;

        // 设置当前所剩体力百分比
        public void SetPercent(float percent)
        {
            foreground.fillAmount = percent;
        }

        /// <summary>
        /// 转换到显示或隐藏的某个状态
        /// </summary>
        /// <param name="state">1 ~ 显示，-1 ~ 隐藏</param>
        public void ToSomeState(int state)
        {
            // 每帧递增的alpha百分比
            float deltaPercent = 1 / switchTime * Time.deltaTime;
            // 当前foreground、background的alpha百分比
            float percent = foreground.color.a / foregroundAlpha;
            // 目标百分比
            percent += deltaPercent * state;
            // 消除越界
            if (percent >= 1 || percent <= 0)
            {
                percent = percent >= 1 ? 1 : 0;
            }

            Color tmp = foreground.color;
            tmp.a = foregroundAlpha * percent;
            foreground.color = tmp;
            tmp.a = backgroundAlpha * percent;
            background.color = tmp;
        }

        private C_Climb climb;
        
        private void Awake()
        {
            foregroundAlpha = foreground.color.a;
            backgroundAlpha = background.color.a;
            climb = GetComponentNotNull<C_Climb>();
        }

        public void ClimbTimeBarSystem()
        {
            SetPercent(climb.climbTimeLeftPercent);
            ToSomeState(climb.climbTimeLeftPercent < 1 ? 1 : -1);
        }
    }
}