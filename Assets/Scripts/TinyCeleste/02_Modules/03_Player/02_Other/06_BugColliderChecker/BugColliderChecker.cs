using System.Collections.Generic;
using System.Linq;
using TinyCeleste._01_Framework;
using TinyCeleste._02_Modules._07_Physics._04_ColliderChecker;
using TinyCeleste._04_Extension._02_Unity;
using UnityEngine;

namespace TinyCeleste._02_Modules._03_Player._02_Other._06_BugColliderChecker
{
    /// <summary>
    /// 玩家被卡入碰撞体内的检测器
    /// </summary>
    public class BugColliderChecker : EntityComponent
    {
        /// <summary>
        /// 探测器
        /// </summary>
        public Collider2D detector;

        /// <summary>
        /// 是否被卡入碰撞体
        /// </summary>
        public bool isHit;

        // 可忽略
        public List<Collider2D> ignoreColliderList;
        public List<E_Tag> ignoreTagList;
        public LayerMask layerMask;

        public void BugColliderCheckerSystem()
        {
            var colliders = detector.GetOverlapCollders(false);
            foreach (var collider in colliders)
            {
                int layer = collider.gameObject.layer;
                // 忽略指定Collider
                if (ignoreColliderList.Contains(collider)) 
                    continue;
                // 忽略特定layer的Collider
                if (layerMask.ContainLayer(layer))
                    continue;
                // 忽略具有特定Tag的Collider
                var colliderTag = collider.GetComponent<C_ColliderTag>();
                if (colliderTag != null)
                {
                    var tagList = colliderTag.tagList;
                    if (ignoreTagList.Any(t1 => tagList.Any(t2 => t1 == t2))) 
                        continue;
                }
                isHit = true;
                return;
            }

            isHit = false;
        }
    }
}