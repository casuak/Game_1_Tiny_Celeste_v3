using UnityEngine;

namespace TinyCeleste._05_MyTool._02_Game
{
    public static class Tool_Input
    {
        /// <summary>
        /// 水平输入
        /// </summary>
        /// <param name="threshold">阈值，绝对值低于阈值时返回0，取值范围0 ~ 1</param>
        /// <returns>0，-1，1</returns>
        public static int GetHorizontalInput(float threshold = 0.1f)
        {
            float tmp = Input.GetAxisRaw("Horizontal");
            int hInput = 0;
            if (Mathf.Abs(tmp) > threshold)
                hInput = tmp > 0 ? 1 : -1;
            return hInput;
        }
        
        /// <summary>
        /// 垂直输入
        /// </summary>
        /// <param name="threshold">阈值，绝对值低于阈值时返回0，取值范围0 ~ 1</param>
        /// <returns>0，-1，1</returns>
        public static int GetVerticalInput(float threshold = 0.1f)
        {
            float tmp = Input.GetAxisRaw("Vertical");
            int hInput = 0;
            if (Mathf.Abs(tmp) > threshold)
                hInput = tmp > 0 ? 1 : -1;
            return hInput;
        }

        /// <summary>
        /// 水平加垂直输入
        /// </summary>
        /// <param name="threshold"></param>
        /// <returns></returns>
        public static Vector2Int GetHVInput(float threshold = 0.1f)
        {
            return new Vector2Int(GetHorizontalInput(threshold), GetVerticalInput(threshold));
        }
    }
}