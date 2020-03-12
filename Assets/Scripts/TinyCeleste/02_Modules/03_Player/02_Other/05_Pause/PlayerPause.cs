using TinyCeleste._01_Framework;
using UnityEngine;

namespace TinyCeleste._02_Modules._03_Player._02_Other._05_Pause
{
    public class PlayerPause : EntityComponent
    {
        // 玩家是否暂停
        public bool isPaused;

        // 玩家是否在暂停的基础上进行了隐藏
        // 如果玩家是隐藏的，那么必然也是暂停的
        public bool isHidden { get; private set; }

        public void Hide()
        {
            if (isHidden) return;
            var srList = GetComponentsInChildren<SpriteRenderer>();
            foreach (var sr in srList)
            {
                sr.enabled = false;
            }

            isPaused = true;
            isHidden = true;
        }

        public void Show()
        {
            if (!isHidden) return;
            var srList = GetComponentsInChildren<SpriteRenderer>();
            foreach (var sr in srList)
            {
                sr.enabled = true;
            }

            isPaused = false;
            isHidden = false;
        }

        public void HideOrShow(bool hide)
        {
            if (hide) Hide();
            else Show();
        }
    }
}