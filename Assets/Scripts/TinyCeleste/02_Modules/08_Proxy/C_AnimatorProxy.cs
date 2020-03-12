using TinyCeleste._01_Framework;
using UnityEngine;

namespace TinyCeleste._02_Modules._08_Proxy
{
    public class C_AnimatorProxy : EntityComponent
    {
        public Animator animator;
        public float beforePause;

        public bool isPaused { get; private set; }

        public void SetInteger(int id, int value)
        {
            animator.SetInteger(id, value);
        }

        // 当前动画是否播放完成
        public bool isFinished => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f;

        public void Pause()
        {
            if (isPaused) return;
            beforePause = animator.speed;
            animator.speed = 0;
            isPaused = true;
        }

        public void Resume()
        {
            if (!isPaused) return;
            animator.speed = beforePause;
            isPaused = false;
        }

        public void PauseOrResume(bool pause)
        {
            if (pause) Pause();
            else Resume();
        }
    }
}