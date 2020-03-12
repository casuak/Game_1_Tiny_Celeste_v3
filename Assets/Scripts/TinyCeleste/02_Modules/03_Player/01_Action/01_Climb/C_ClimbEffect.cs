using TinyCeleste._01_Framework;
using TinyCeleste._02_Modules._03_Player._01_Action._04_Face;
using TinyCeleste._02_Modules._04_Effect._01_Dust;
using TinyCeleste._02_Modules._07_Physics._04_ColliderChecker;
using TinyCeleste._02_Modules._08_Proxy;
using UnityEngine;

namespace TinyCeleste._02_Modules._03_Player._01_Action._01_Climb
{
    public class C_ClimbEffect : EntityComponent
    {
        // 创建特效的时间间隔
        public float createInterval = 0.15f;

        // 间隔计时器
        public float createTimer;

        // 特效位置
        public Vector2 effectPosition = Vector2.zero;

        // 特效缩放
        public Vector3 effectScale = new Vector3(1, 0.9f, 1);

        // 特效移动速率
        public float effectFlowSpeed = 2;

        private new Transform transform;
        private Face face;
        private ColliderCheckerItem groundChecker;

        private void Awake()
        {
            transform = GetComponentNotNull<C_Transform2DProxy>().transform;
            face = GetComponentNotNull<Face>();
            groundChecker = GetComponentNotNull<C_ColliderChecker>().GetChecker("Ground Checker");
        }

        public void ClimbEffectSystem()
        {
            if (groundChecker.isHit) return;
            var deltaTime = Time.deltaTime;
            if (createTimer >= createInterval)
            {
                createTimer = 0;
                // 创建特效
                var position = (Vector2) transform.position + effectPosition;
                var flowDirection = Vector2.right * (int) face.faceEnum;
                var color = Color.white;
                var dust = S_Dust_Factory.Instance.CreateDust(position, flowDirection, color, effectScale);
                dust.SetFlowSpeed(effectFlowSpeed);
            }
            else
            {
                createTimer += deltaTime;
            }
        }
    }
}