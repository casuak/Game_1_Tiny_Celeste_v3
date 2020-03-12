using System;
using Casuak.Extension._01_UnityComponent;
using TinyCeleste._01_Framework;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TinyCeleste._02_Modules._02_Camera
{
    public class C_CameraShake : EntityComponent
    {
        // 是否正在抖动
        private bool m_IsShaking;

        // 抖动的时间
        private float m_ShakeTime;

        // 抖动对象的transform
        public Transform shakeTarget;

        private CameraShakeParams m_CurrentShakeParams;
        public CameraShakeParams shakeParams_Dash;
        public CameraShakeParams shakeParams_Die;
        public C_CameraRippleEffect rippleEffect;

        public float timer;

        public enum ShakeType
        {
            Dash,
            Die
        }

        [Serializable]
        public class CameraShakeParams
        {
            public float shakeTime;
            public float shakePower;
        }

        public void CameraShakeSystem(float deltaTime)
        {
            if (timer > 0)
            {
                var randRad = Random.value * 2 * Mathf.PI;
                var randVec = new Vector2(Mathf.Cos(randRad), Mathf.Sin(randRad));
                randVec = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
//                randVec = Vector2.Lerp(shakeTarget.position, randVec, m_CurrentShakeParams.lerp);
                shakeTarget.SetLocalPositionXY(randVec * m_CurrentShakeParams.shakePower);

                timer -= deltaTime;
                if (timer <= 0)
                {
                    timer = 0;
                    shakeTarget.SetLocalPositionXY(Vector2.zero);
                    rippleEffect.Stop();
                }
            }
        }

        // 开始一次抖动
        public void StartShake(ShakeType shakeType)
        {
            switch (shakeType)
            {
                case ShakeType.Dash:
                    m_CurrentShakeParams = shakeParams_Dash;
                    break;
                case ShakeType.Die:
                    m_CurrentShakeParams = shakeParams_Die;
                    break;
                default:
                    throw new Exception("Error");
            }

            rippleEffect.Emit();
            timer = m_CurrentShakeParams.shakeTime;
        }

        // 停止抖动
        public void StopShake()
        {
            timer = 0;
        }
    }
}