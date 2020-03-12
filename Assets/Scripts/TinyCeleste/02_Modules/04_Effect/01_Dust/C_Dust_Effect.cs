using TinyCeleste._01_Framework;
using UnityEngine;

namespace TinyCeleste._02_Modules._04_Effect._01_Dust
{
    public class C_Dust_Effect : EntityComponent
    {
        // 灰尘流动的方向
        public Vector2 flowDirection = Vector2.zero;

        // 灰尘流动的速率
        public float flowSpeed = 0;

        private Animator animator;

        private void Awake()
        {
            animator = GetComponentNotNull<Animator>();
        }

        public void DustEffectSystem()
        {
            var deltaTime = Time.deltaTime;
            // 灰尘朝着指定的方向流动
            transform.Translate(flowSpeed * deltaTime * flowDirection);
            // 灰尘动画播放结束时自动销毁
            var animatorInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (animatorInfo.normalizedTime >= 1.0f)
            {
                Destroy(gameObject);
            }
        }
    }
}