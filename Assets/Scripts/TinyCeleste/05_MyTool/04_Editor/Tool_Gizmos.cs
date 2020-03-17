using UnityEngine;

namespace TinyCeleste._05_MyTool._04_Editor
{
    public static class Tool_Gizmos
    {
        /// <summary>
        /// 线段类
        /// </summary>
        public class LineSegment
        {
            public Vector2 vertex1 = Vector2.zero;
            public Vector2 vertex2 = Vector2.zero;

            public void DrawSelf()
            {
                Gizmos.DrawLine(vertex1, vertex2);
            }
        }
        
        /// <summary>
        /// 绘制多边形
        /// </summary>
        /// <param name="color">颜色</param>
        /// <param name="positions">顶点坐标</param>
        public static void DrawPolygon(Color color, params Vector2[] positions)
        {
            Gizmos.color = color;
            if (positions.Length < 2)
            {
                Debug.Log("绘制多边形至少需要2个顶点");
                return;
            }

            for (int i = 0; i < positions.Length; i++)
            {
                Vector2 p1 = positions[i];
                Vector2 p2 = i + 1 < positions.Length ? positions[i + 1] : positions[0];
                Gizmos.DrawLine(p1, p2);
            }
        }

        /// <summary>
        /// 绘制矩阵
        /// </summary>
        /// <param name="color">颜色</param>
        /// <param name="center">矩阵中心</param>
        /// <param name="w">宽度</param>
        /// <param name="h">高度</param>
        public static void DrawRectangle(Color color, Vector2 center, float w, float h)
        {
            float half_w = w / 2;
            float half_h = h / 2;
            Vector2 leftUp = center + Vector2.left * half_w + Vector2.up * half_h;
            Vector2 rightUp = center + Vector2.right * half_w + Vector2.up * half_h;
            Vector2 rightDown = center + Vector2.right * half_w + Vector2.down * half_h;
            Vector2 leftDown = center + Vector2.left * half_w + Vector2.down * half_h;
            Tool_Gizmos.DrawPolygon(color, leftUp, rightUp, rightDown, leftDown);
        }
        
        /// <summary>
        /// 绘制圆角矩形
        /// </summary>
        /// <param name="center">矩形的中心</param>
        /// <param name="w">矩形的宽度</param>
        /// <param name="h">矩形的高度</param>
        /// <param name="r">圆角的半径</param>
        public static void DrawRoundedRect(Color color, Vector2 center, float w, float h, float r)
        {
            w = w / 2;
            h = h / 2;
            Gizmos.color = color;
            LineSegment line = new LineSegment();
            // Right
            line.vertex1 = center + new Vector2(w + r, h);
            line.vertex2 = center + new Vector2(w + r, -h);
            line.DrawSelf();
            // Left
            line.vertex1 = center + new Vector2(-w - r, h);
            line.vertex2 = center + new Vector2(-w - r, -h);
            line.DrawSelf();
            // Top
            line.vertex1 = center + new Vector2(-w, h + r);
            line.vertex2 = center + new Vector2(w, h + r);
            line.DrawSelf();
            // Bottom
            line.vertex1 = center + new Vector2(-w, -h - r);
            line.vertex2 = center + new Vector2(w, -h - r);
            line.DrawSelf();
            // Right Top
            DrawCircle(color, center + new Vector2(w, h), r, Mathf.PI * 0f, Mathf.PI * 0.5f);
            // Left Top
            DrawCircle(color, center + new Vector2(-w, h), r, Mathf.PI * 0.5f, Mathf.PI * 1f);
            // Left Bottom
            DrawCircle(color, center + new Vector2(-w, -h), r, Mathf.PI * 1f, Mathf.PI * 1.5f);
            // Right Bottom
            DrawCircle(color, center + new Vector2(w, -h), r, Mathf.PI * 1.5f, Mathf.PI * 2f);
        }

        /// <summary>
        /// 绘制圆形
        /// </summary>
        /// <param name="color">颜色</param>
        /// <param name="center">圆心坐标</param>
        /// <param name="r">半径</param>
        /// <param name="deltaRad">每隔多少弧度画一条线段</param>
        public static void DrawCircle(Color color, Vector2 center, float r, float deltaRad = 0.1f)
        {
            DrawCircle(color, center, r, 0, 2 * Mathf.PI, deltaRad);
        }

        /// <summary>
        /// 绘制圆形
        /// </summary>
        /// <param name="color">颜色</param>
        /// <param name="center">圆心坐标</param>
        /// <param name="r">半径</param>
        /// <param name="startRad">开始弧度</param>
        /// <param name="endRad">结束弧度</param>
        /// <param name="deltaRad">每隔多少弧度画一条线段</param>
        public static void DrawCircle(Color color, Vector2 center, float r, float startRad, float endRad, float deltaRad = 0.1f)
        {
            Gizmos.color = color;
            for (; startRad < endRad; startRad += deltaRad)
            {
                Vector2 from = AngleToCord(center, r, startRad);
                Vector2 to = AngleToCord(center, r, startRad + deltaRad);
                Gizmos.DrawLine(from, to);
            }
        }

        /// <summary>
        /// 将圆内的弧度(0 ~ 2 * PI)转换为世界坐标
        /// </summary>
        /// <param name="center">圆心</param>
        /// <param name="r">半径</param>
        /// <param name="rad">弧度</param>
        /// <returns></returns>
        public static Vector2 AngleToCord(Vector2 center, float r, float rad)
        {
            return center + new Vector2(r * Mathf.Cos(rad), r * Mathf.Sin(rad));
        }
    }
}