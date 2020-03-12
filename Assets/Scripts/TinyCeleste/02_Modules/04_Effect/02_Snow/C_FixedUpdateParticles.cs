using System;
using System.Collections.Generic;
using TinyCeleste._01_Framework;
using UnityEngine;

namespace TinyCeleste._02_Modules._04_Effect._02_Snow
{
    public class C_FixedUpdateParticles : EntityComponent
    {
        private Transform m_Transform;
        private ParticleSystem m_ParticleSystem;
        private ParticleSystem.Particle[] m_particles;

        public Transform target_Trans;  //目标位置.(手动拖拽)
        private Vector3 pos;            //粒子移动的目标位置.

        void Start()
        {
            m_Transform = gameObject.GetComponent<Transform>();
            m_ParticleSystem = gameObject.GetComponent<ParticleSystem>();
            m_particles = new ParticleSystem.Particle[m_ParticleSystem.main.maxParticles];  //实例化，个数为粒子系统设置的最大粒子数.

            //ParticleSystem.MainModule main = m_ParticleSystem.main;
            //main.simulationSpace = ParticleSystemSimulationSpace.Custom;    //设置模拟空间为定制.
            //main.customSimulationSpace = target_Trans;                      //定制的模拟空间为目标位置Transform.

            pos = m_Transform.InverseTransformPoint(target_Trans.position); //粒子系统模拟空间设置为Local时，需要把目标位置换成粒子系统的本地坐标.

            //pos = target_Trans.position;  //粒子系统模拟空间为World时，直接把目标位置赋值给pos.
        }

        private void LateUpdate()
        {
            //获取当前激活的粒子.
            int num = m_ParticleSystem.GetParticles(m_particles);

            //设置粒子移动.
            for (int i = 0; i < num; i++) 
            {
                m_particles[i].position += -Time.deltaTime * m_particles[i].velocity;
            }

            //重新赋值粒子.
            m_ParticleSystem.SetParticles(m_particles, num);
        }

        void FixedUpdate()
        {
            //获取当前激活的粒子.
            int num = m_ParticleSystem.GetParticles(m_particles);

            //设置粒子移动.
            for (int i = 0; i < num; i++) 
            {
                m_particles[i].position += Time.deltaTime * m_particles[i].velocity;
            }

            //重新赋值粒子.
            m_ParticleSystem.SetParticles(m_particles, num);
        }
    }
}