using System.Collections.Generic;
using TinyCeleste._01_Framework;
using UnityEngine;

namespace TinyCeleste._02_Modules._04_Effect._01_Dust
{
    public class S_Dust_Factory : EntitySingleton<S_Dust_Factory>
    {
        public E_Dust m_Prefab;
        public List<E_Dust> m_DustList;

        public E_Dust CreateDust()
        {
            var dust = Instantiate(m_Prefab).GetComponent<E_Dust>();
            dust.SetParent(transform);
            m_DustList.Add(dust);
            return dust;
        }
        
        public E_Dust CreateDust(Vector2 position)
        {
            var dust = CreateDust();
            dust.transform.position = position;
            return dust;
        }

        public E_Dust CreateDust(Vector2 position, Vector2 flowDirection, Color color)
        {
            var dust = CreateDust();
            dust.Init(transform, position, flowDirection, color);
            return dust;
        }

        public E_Dust CreateDust(Vector2 position, Vector2 flowDirection, Color color, Vector3 localScale)
        {
            var dust = CreateDust();
            dust.Init(transform, position, flowDirection, color, localScale);
            return dust;
        }
    }
}