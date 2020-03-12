using System;
using System.Collections.Generic;
using System.Linq;
using TinyCeleste._01_Framework;
using UnityEngine;

namespace TinyCeleste._02_Modules._07_Physics._04_ColliderChecker
{
    public class C_ColliderChecker : EntityComponent
    {
        public List<ColliderCheckerItem> checkers;

        public ColliderCheckerItem GetChecker(string checkerName)
        {
            foreach (var checker in checkers)
            {
                if (checker.name.Equals(checkerName)) return checker;
            }

            return null;
        }

        public void ColliderCheckerSystem()
        {
            foreach (var item in checkers)
            {
                item.ColliderCheckerSystem();
            }
        }
    }

    [Serializable]
    public class ColliderCheckerItem
    {
        public string name = "Ground Checker";

        // 需要进行检测的带有该Tag的碰撞体
        public List<E_Tag> acceptTags;

        // 触发器
        public Collider2D collider;

        // 每帧更新，表示是否与目标碰撞体发生碰撞
        public bool isHit;

        // 碰撞事件（上一帧无碰撞，这一帧碰撞，则触发）
        public bool hitEvent;

        // 碰撞到的物体列表
        public List<C_ColliderTag> targetList;
        public C_ColliderTag firstTarget => targetList[0];
        public List<Collider2D> targetColliderList;
        public Collider2D firstTargetCollider => targetColliderList[0];

        // 仅检测一个目标碰撞体
        public void ColliderCheckerSystem()
        {
            var contacts = collider.GetOverlapCollders();
            targetList.Clear();
            targetColliderList.Clear();
            for (int i = 0; i < contacts.Length; i++)
            {
                var collider = contacts[i];
                var colliderTag = collider.GetComponent<C_ColliderTag>();
                if (colliderTag == null) continue;
                if (colliderTag.tagList.Any(t1 => acceptTags.Any(t2 => t1 == t2)))
                {
                    targetList.Add(colliderTag);
                    targetColliderList.Add(collider);
                }
            }

            hitEvent = targetList.Count > 0 && !isHit;
            isHit = targetList.Count > 0;
        }
    }
}