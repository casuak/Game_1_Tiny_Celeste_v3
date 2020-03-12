using TinyCeleste._01_Framework;
using UnityEngine;

namespace TinyCeleste._02_Modules._01_Common
{
    /// <summary>
    /// 内部为空的盒子碰撞器
    /// 原理为将boxCollider设置为trigger
    /// 将boxCollider的属性copy到edgeCollider上
    /// </summary>
    public class C_EmptyBoxCollider2D : EntityComponent
    {
        public BoxCollider2D boxCollider2D;
        public EdgeCollider2D edgeCollider2D;

        [ContextMenu("Reset Edge Collider")]
        private void ResetEdgeCollider()
        {
            var b = boxCollider2D;
            var e = edgeCollider2D;
            var halfW = b.size.x / 2;
            var halfH = b.size.y / 2;
            // 从左上到左下一个循环
            var p1 = b.offset + new Vector2(-halfW, halfH);
            var p2 = b.offset + new Vector2(halfW, halfH);
            var p3 = b.offset + new Vector2(halfW, -halfH);
            var p4 = b.offset + new Vector2(-halfW, -halfH);
            var p5 = p1;
            e.points = new[] {p1, p2, p3, p4, p5};
        }
    }
}