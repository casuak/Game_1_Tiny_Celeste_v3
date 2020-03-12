using System.Collections.Generic;
using TinyCeleste._01_Framework;
using TinyCeleste._02_Modules._07_Physics._04_ColliderChecker;
using UnityEngine;

namespace TinyCeleste._02_Modules._03_Player._01_Action._02_Dash
{
    public class DashCount : EntityComponent
    {
        // 可恢复的最大冲刺速度
        public int maxCount;

        // 当前的冲刺次数
        public int count;

        // 将次数转换为颜色
        public List<Color> countToColor;
        
        // 当前颜色
        public Color currentColor => countToColor[count];

        private ColliderCheckerItem groundChecker;

        private void Awake()
        {
            groundChecker = GetComponentNotNull<C_ColliderChecker>().GetChecker("Ground Checker");
        }

        public void ResumeDashCountSystem()
        {
            if (groundChecker.isHit)
            {
                ResumeDashCount();
            }
        }

        public bool ResumeDashCount()
        {
            bool res = count < maxCount;
            count = maxCount;
            return res;
        }
    }
}