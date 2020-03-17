using UnityEngine;

namespace TinyCeleste._04_Extension._01_UnityComponent
{
    public static partial class EX_Transform
    {
        /// <summary>
        /// 返回2D坐标
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static Vector2 GetPos2D(this Transform transform)
        {
            return transform.position;
        }

        /// <summary>
        /// 设置2D坐标
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="pos"></param>
        public static void SetPos2D(this Transform transform, Vector2 pos)
        {
            transform.SetPositionXY(pos);
        }
        
        /// <summary>
        /// 清理Transform的孩子
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="immediate"></param>
        public static void ClearChildren(this Transform transform, bool immediate = false)
        {
            GameObject[] tmp = new GameObject[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                tmp[i] = transform.GetChild(i).gameObject;
            }

            foreach (var gameObject in tmp)
            {
                if (immediate)
                    Object.DestroyImmediate(gameObject);
                else
                    Object.Destroy(gameObject);
            }
        }

        /// <summary>
        /// Z轴旋转
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="angle"></param>
        public static void Rotate2D(this Transform transform, float angle)
        {
            transform.RotateAround(transform.position, Vector3.forward, angle);
        }

        public static void SetLocalPositionXY(this Transform transform, Vector2 position)
        {
            transform.SetLocalPositionX(position.x);
            transform.SetLocalPositionY(position.y);
        }

        public static void SetLocalPositionX(this Transform transform, float value)
        {
            Vector3 position = transform.localPosition;
            position.x = value;
            transform.localPosition = position;
        }

        public static void SetLocalPositionY(this Transform transform, float value)
        {
            Vector3 position = transform.localPosition;
            position.y = value;
            transform.localPosition = position;
        }

        public static void SetPositionXY(this Transform transform, Vector2 position)
        {
            transform.SetPositionX(position.x);
            transform.SetPositionY(position.y);
        }

        public static void SetPositionX(this Transform transform, float x)
        {
            Vector3 position = transform.position;
            position.x = x;
            transform.position = position;
        }

        public static void SetPositionY(this Transform transform, float y)
        {
            Vector3 position = transform.position;
            position.y = y;
            transform.position = position;
        }

        public static void SetPositionZ(this Transform transform, float z)
        {
            Vector3 position = transform.position;
            position.z = z;
            transform.position = position;
        }

        public static void SetScaleX(this Transform transform, float x)
        {
            Vector3 scale = transform.localScale;
            scale.x = x;
            transform.localScale = scale;
        }

        public static void SetScaleY(this Transform transform, float y)
        {
            Vector3 scale = transform.localScale;
            scale.y = y;
            transform.localScale = scale;
        }

        public static void SetScaleZ(this Transform transform, float z)
        {
            Vector3 scale = transform.localScale;
            scale.z = z;
            transform.localScale = scale;
        }
    }
}