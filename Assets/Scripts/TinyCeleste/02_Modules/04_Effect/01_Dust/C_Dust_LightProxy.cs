using System;
using Light2D;
using TinyCeleste._01_Framework;
using UnityEngine;

namespace TinyCeleste._02_Modules._04_Effect._01_Dust
{
    public class C_Dust_LightProxy : EntityComponent
    {
        public LightSprite lightSprite;

        // 是否被点亮
        public bool isLighted;

        // 衰减（每次乘这个数）
        public float decreaseSpeed = 0.5f;

        public LightType lightType = LightType.Point;

        public enum LightType
        {
            Point,
            Ambient
        }

        private void Awake()
        {
            UpdateLight();
        }

        private void Update()
        {
            UpdateLight();
        }

        private void UpdateLight()
        {
            if (isLighted)
                EnableLight(lightType);
            else
                DisableLight(lightType);
            // Disable Other Light
            foreach (var value in Enum.GetValues(typeof(LightType)))
            {
                var lightType = (LightType) value;
                if (lightType != this.lightType)
                    DisableLight(lightType);
            }
        }

        private void EnableLight(LightType lightType)
        {
            switch (lightType)
            {
                case LightType.Point:
                    lightSprite.gameObject.SetActive(true);
                    break;
                case LightType.Ambient:
                    gameObject.layer = LayerMask.NameToLayer("Ambient Light");
                    break;
                default:
                    return;
            }
        }

        private void DisableLight(LightType lightType)
        {
            switch (lightType)
            {
                case LightType.Point:
                    lightSprite.gameObject.SetActive(false);
                    break;
                case LightType.Ambient:
                    gameObject.layer = LayerMask.NameToLayer("Default");
                    break;
                default:
                    return;
            }
        }

        public void LightDecreaseSystem()
        {
            lightSprite.Color *= decreaseSpeed;
        }

        public void SetLightColor(Color color)
        {
            lightSprite.Color = color;
        }
    }
}