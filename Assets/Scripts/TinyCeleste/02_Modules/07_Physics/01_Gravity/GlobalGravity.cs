using System;
using System.Collections.Generic;
using TinyCeleste._01_Framework;

namespace TinyCeleste._02_Modules._07_Physics._01_Gravity
{
    public class GlobalGravity : EntitySingleton<GlobalGravity>
    {
        public Dictionary<E_GravityType, GravityParams> dictionary;
        
        public GravityParams GetGravityParams(E_GravityType gravityType)
        {
            return dictionary[gravityType];
        }

        public GravityParams normalGravity = new GravityParams(100, true, 20);
        public GravityParams waterGravity = new GravityParams(10, true, 10);
        public GravityParams zeroGravity = new GravityParams(0, false, 0);

        public enum E_GravityType
        {
            Normal,
            Water,
            Zero
        }

        protected override void Awake()
        {
            base.Awake();
            dictionary = new Dictionary<E_GravityType, GravityParams>()
            {
                {E_GravityType.Normal, normalGravity},
                {E_GravityType.Water, waterGravity},
                {E_GravityType.Zero, zeroGravity}
            };
        }

        [Serializable]
        public class GravityParams
        {
            public float gravity;     // 重力加速度
            public bool hasMaxSpeed;  // 受重力影响下是否具有y轴最大速度
            public float maxSpeed;    // y轴最大速度（hasMaxSpeed==false时该值无效）

            public GravityParams(float gravity, bool hasMaxSpeed, float maxSpeed)
            {
                this.gravity = gravity;
                this.hasMaxSpeed = hasMaxSpeed;
                this.maxSpeed = maxSpeed;
            }
        }
    }
}