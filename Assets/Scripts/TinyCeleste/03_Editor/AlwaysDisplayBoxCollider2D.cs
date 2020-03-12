using System.Collections.Generic;
using Casuak.MyTool._04_Editor;
using UnityEngine;

namespace TinyCeleste._03_Editor
{
    /// <summary>
    /// 总是显示gameobject上以及子物体上的boxCollider2d线框
    /// </summary>
    public class AlwaysDisplayBoxCollider2D : MonoBehaviour
    {
        public bool enable = true;
        public bool displayChild = true;
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!enable) return;
            List<BoxCollider2D> colliderList = new List<BoxCollider2D>();
            colliderList.AddRange(GetComponents<BoxCollider2D>());
            if (displayChild)
                colliderList.AddRange(GetComponentsInChildren<BoxCollider2D>());
            foreach (var collider in colliderList)
            {
                // 准备数据
                Vector2 pos = collider.transform.position;
                Vector2 offset = collider.offset;
                Vector2 size = collider.size;
                float edgeRadius = collider.edgeRadius;
                // 计算绘制坐标
                Vector2 center = pos + offset;
                // 绘制矩形
                if (edgeRadius < 0.000001f)
                    Tool_Gizmos.DrawRectangle(Color.red, center, size.x, size.y);
                // 绘制圆角矩形
                else
                    Tool_Gizmos.DrawRoundedRect(Color.red, center, size.x, size.y, edgeRadius);
            }
        }
#endif
    }
}