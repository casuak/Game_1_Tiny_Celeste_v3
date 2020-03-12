using System;
using TinyCeleste._01_Framework;
using TinyCeleste._02_Modules._08_Proxy;
using UnityEngine;

namespace TinyCeleste._02_Modules._02_Camera
{
    public class S_MainCamera : EntitySingleton<S_MainCamera>
    {
        public C_CameraFollow follow;
        public C_Transform2DProxy transform2DProxy;
        public C_CameraWrapper cameraWrapper;
        public C_CameraShake cameraShake;
        public C_Rigidbody2DProxy rigidbody2DProxy;

        public void Shake(C_CameraShake.ShakeType shakeType)
        {
            cameraShake.StartShake(shakeType);
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            cameraShake.CameraShakeSystem(deltaTime);
        }

        private void FixedUpdate()
        {
            var deltaTime = Time.fixedDeltaTime;
            follow.FollowSystem(deltaTime);
        }
    }
}