using TinyCeleste._01_Framework;
using TinyCeleste._02_Modules._03_Player._01_Action._04_Face;
using TinyCeleste._02_Modules._03_Player._01_Action._05_Jump;
using TinyCeleste._02_Modules._04_Effect._01_Dust;
using TinyCeleste._02_Modules._07_Physics._04_ColliderChecker;
using TinyCeleste._02_Modules._08_Proxy;
using UnityEngine;

namespace TinyCeleste._02_Modules._03_Player._02_Other._03_FootDust
{
    public class FootDust : EntityComponent
    {
        // 生成Dust相对于player的位置
        public Vector2 dustPosition = new Vector2(0, -0.38f);

        // 缩放比例
        public float dustScale = 0.5f;

        // 生成尘土特效的移动速度
        public float dustFlowSpeed = 3f;

        private C_Transform2DProxy transform2DProxy;
        private Face face;
        private ColliderCheckerItem groundChecker;
        private Jump jump;
        private C_Rigidbody2DProxy m_Rigidbody2DWrapper;

        private void Awake()
        {
            transform2DProxy = GetComponentNotNull<C_Transform2DProxy>();
            face = GetComponentNotNull<Face>();
            groundChecker = GetComponentNotNull<C_ColliderChecker>().GetChecker("Ground Checker");
            jump = GetComponentNotNull<Jump>();
            m_Rigidbody2DWrapper = GetComponentNotNull<C_Rigidbody2DProxy>();
        }

        public void FootDustSystem()
        {
            // 着陆或起跳时，在脚底位置产生一个灰尘特效
            if (groundChecker.hitEvent && m_Rigidbody2DWrapper.velocity.y < 0.001f || jump.jumpEvent)
            {
                var transform = transform2DProxy.transform;
                var offset = new Vector2((int) face.faceEnum * dustPosition.x,
                    dustPosition.y);
                var position = (Vector2) transform.position + offset;
                var localScale = new Vector3((int) face.faceEnum, 1, 1) * dustScale;
                var flowDirection = (int) face.faceEnum * Vector2.left;
                S_Dust_Factory.Instance.CreateDust(position, flowDirection, Color.white, localScale)
                    .SetLightActive(true)
                    .SetLightColor(Color.white)
                    .SetFlowSpeed(dustFlowSpeed);
            }
        }
    }
}