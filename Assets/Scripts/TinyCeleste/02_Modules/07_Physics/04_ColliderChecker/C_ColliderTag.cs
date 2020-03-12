using System.Collections.Generic;
using TinyCeleste._01_Framework;
using UnityEngine;

namespace TinyCeleste._02_Modules._07_Physics._04_ColliderChecker
{
    public class C_ColliderTag : EntityComponent
    {
        public List<E_Tag> tagList;

        // 寻找Tag所依附的（parent路径上）第一个GoEntity
        public Entity GetEntityObject()
        {
            Transform t = transform;
            while (t != null)
            {
                var goEntity = t.GetComponent<Entity>();
                if (goEntity != null) return goEntity;
                t = t.parent;
            }

            return null;
        }

        public void AddTag(E_Tag tag)
        {
            foreach (var eTag in tagList)
            {
                if (eTag == tag) return;
            }

            tagList.Add(tag);
        }

        public void RemoveTag(E_Tag tag)
        {
            tagList.Remove(tag);
        }
    }

    public enum E_Tag
    {
        Player, // 玩家
        Wall, // 墙面
        Ground, // 地面
        SharpPoint, // 尖刺
        Platform, // 可从下方穿过的平台
        Movable, // 可以被移动
    }
}