using System;
using System.Collections.Generic;
using TinyCeleste._01_Framework;
using UnityEngine;

namespace TinyCeleste._02_Modules._04_Effect._02_Snow
{
    public class C_FixedUpdateParticles : EntityComponent
    {
        private ParticleSystem m_ParticleSystem;
        private ParticleSystem.Particle[] m_particles;

        public Transform target_Trans; //目标位置.(手动拖拽)

        private void Start()
        {
            m_ParticleSystem = gameObject.GetComponent<ParticleSystem>();
            m_particles = new ParticleSystem.Particle[m_ParticleSystem.main.maxParticles];
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

        private void FixedUpdate()
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