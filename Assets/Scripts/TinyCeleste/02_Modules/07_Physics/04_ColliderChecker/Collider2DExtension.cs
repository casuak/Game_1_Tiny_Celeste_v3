using System.Collections.Generic;
using Casuak.Extension;
using Casuak.Extension._03_CSharp;
using UnityEngine;

namespace TinyCeleste._02_Modules._07_Physics._04_ColliderChecker
{
    public static class Collider2DExtension
    {
        // 实时返回与当前碰撞体发生碰撞且具有acceptTags中的任意一个E_Tag的Tag列表
        public static List<C_ColliderTag> GetTags(this Collider2D collider, List<E_Tag> acceptTags)
        {
            var result = new Collider2D[100];
            var filter = new ContactFilter2D().NoFilter();

            int num = collider.OverlapCollider(filter, result);
            var tagList = new List<C_ColliderTag>();
            for(int j = 0;j < num;j ++)
            {
                var tag = result[j].GetComponent<C_ColliderTag>();
                if (tag != null && EX_List.Match(tag.tagList, acceptTags))
                {
                    tagList.Add(tag);
                }
            }

            return tagList;
        }

        public static Collider2D[] GetOverlapCollders(this Collider2D collider, bool includeTrigger = true)
        {
            var contacts = new Collider2D[100];
            var filter = new ContactFilter2D().NoFilter();
            filter.useTriggers = includeTrigger;
            
            int num = collider.OverlapCollider(filter, contacts);
            var result = new Collider2D[num];
            for (int i = 0; i < num; i++)
            {
                result[i] = contacts[i];
            }

            return result;
        }
    }
}