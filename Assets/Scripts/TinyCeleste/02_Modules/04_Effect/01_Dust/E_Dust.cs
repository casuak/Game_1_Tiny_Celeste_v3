using System.Collections.Generic;
using TinyCeleste._01_Framework;
using TinyCeleste._02_Modules._04_Effect._01_Dust._01_Event;
using TinyCeleste._04_Extension._01_UnityComponent;
using UnityEngine;

namespace TinyCeleste._02_Modules._04_Effect._01_Dust
{
    public class E_Dust : Entity
    {
        public C_Dust_Effect dustEffect;
        public SpriteRenderer spriteRenderer;
        public Animator animator;
        public C_Dust_LightProxy lightProxy;

        // 监听当前Dust摧毁事件的对象
        private readonly List<I_OnDustDestroy> observerList = new List<I_OnDustDestroy>();

        public void AddObserver(I_OnDustDestroy observer)
        {
            observerList.Add(observer);
        }
        
        private void OnDestroy()
        {
            foreach (var observer in observerList)
            {
                observer.OnDustDestroy();
            }
        }

        private void Update()
        {
            dustEffect.DustEffectSystem();
            lightProxy.LightDecreaseSystem();
        }

        public E_Dust Init(Transform parent, Vector2 position, Vector2 flowDirection, Color color)
        {
            var t = transform;
            t.SetPositionXY(position);
            t.parent = parent;
            SetColor(color);
            SetFlowDirection(flowDirection);
            return this;
        }

        public E_Dust Init(Transform parent, Vector2 position, Vector2 flowDirection, Color color, Vector3 localScale)
        {
            Init(parent, position, flowDirection, color);
            transform.localScale = localScale;
            return this;
        }
        
        public E_Dust SetColor(Color color)
        {
            spriteRenderer.color = color;
            return this;
        }

        public E_Dust SetFlowDirection(Vector2 flowDirection)
        {
            dustEffect.flowDirection = flowDirection;
            return this;
        }

        public E_Dust SetSpriteOrderInLayer(int order)
        {
            spriteRenderer.sortingOrder = order;
            return this;
        }

        public E_Dust SetFlowSpeed(float speed)
        {
            dustEffect.flowSpeed = speed;
            return this;
        }

        public E_Dust SetParent(Transform parent)
        {
            transform.parent = parent;
            return this;
        }

        public E_Dust SetAnimatorSpeed(float value)
        {
            animator.speed = value;
            return this;
        }

        public E_Dust SetLightType(C_Dust_LightProxy.LightType lightType)
        {
            lightProxy.lightType = lightType;
            return this;
        }

        public E_Dust CloseLightDecrease()
        {
            lightProxy.decreaseSpeed = 1;
            return this;
        }
        
        // 设置光效，默认关闭
        public E_Dust SetLightActive(bool active)
        {
            lightProxy.isLighted = active;
            return this;
        }

        public E_Dust SetLightColor(Color color)
        {
            lightProxy.SetLightColor(color);
            return this;
        }
    }
}