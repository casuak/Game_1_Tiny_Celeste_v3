using System.Collections.Generic;
using TinyCeleste._01_Framework;
using TinyCeleste._02_Modules._07_Physics._04_ColliderChecker;
using TinyCeleste._02_Modules._08_Proxy;
using UnityEngine;

namespace TinyCeleste._02_Modules._05_PrefabTile._04_MovePlatform
{
    /// <summary>
    /// 抓取和释放与移动平台上方发生的且具有movable标签的物体
    /// </summary>
    public class C_GrabOrReleaseChild : EntityComponent
    {
        private new Transform transform;
        private ColliderCheckerItem movableChecker;

        public List<TakeInChildParam> childList = new List<TakeInChildParam>();

        private void Awake()
        {
            transform = GetComponentNotNull<C_Transform2DProxy>().transform;
            movableChecker = GetComponentNotNull<C_ColliderChecker>().GetChecker("Movable Checker");
        }

        public void TakeInChildrenSystem()
        {
            // 重置检查状态
            foreach (var param in childList)
            {
                param.isCheck = false;
            }
            // 当前碰撞检查
            var tmpChildList = new List<TakeInChildParam>();
            foreach (var tagContainer in movableChecker.targetList)
            {
                var childRoot = tagContainer.GetEntityObject().transform;
                if (childRoot == null) childRoot = tagContainer.transform;
                var param = GetParam(childRoot);
                // 已存在
                if (param != null)
                {
                    param.isCheck = true;
                }
                // 新加入
                else
                {
                    param = TakeInChild(childRoot);
                }
                tmpChildList.Add(param);
            }
            // 剔除
            foreach (var param in childList)
            {
                if (param.isCheck) continue;
                GiveUpChild(param);
            }
            // 替换
            childList = tmpChildList;
        }

        private TakeInChildParam GetParam(Transform childRoot)
        {
            foreach (var param in childList)
            {
                if (param.childRoot == childRoot) return param;
            }

            return null;
        }
        
        private TakeInChildParam TakeInChild(Transform childRoot)
        {
            var param = new TakeInChildParam(childRoot, childRoot.parent);
            param.isCheck = true;
            childRoot.parent = transform;
            return param;
        }

        private void GiveUpChild(TakeInChildParam param)
        {
            param.childRoot.parent = param.oldParent;
        }
        
        public class TakeInChildParam
        {
            public Transform childRoot;
            public Transform oldParent;
            public bool isCheck;

            public TakeInChildParam(Transform childRoot, Transform oldParent)
            {
                this.childRoot = childRoot;
                this.oldParent = oldParent;
            }
        }
    }
}