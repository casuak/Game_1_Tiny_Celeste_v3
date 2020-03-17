using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

namespace TinyCeleste._04_Extension._01_UnityComponent
{
    public static partial class EX_Transform
    {
        /// <summary>
        /// 编辑模式下使用的可以Undo的清理孩子方法
        /// </summary>
        /// <param name="transform"></param>
        public static void ClearChildren_Editor(this Transform transform)
        {
            GameObject[] tmp = new GameObject[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                tmp[i] = transform.GetChild(i).gameObject;
            }

            foreach (var gameObject in tmp)
            {
                Undo.DestroyObjectImmediate(gameObject);
            }
        }
    }
}

#endif