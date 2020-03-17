using UnityEngine;

namespace TinyCeleste._04_Extension._02_Unity
{
    public static class EX_Vector2
    {
        public static bool EqualZero(this Vector2 vector2)
        {
            return Mathf.Abs(vector2.x) < Mathf.Epsilon && Mathf.Abs(vector2.y) < Mathf.Epsilon;
        }

        public static Vector2 Abs(this Vector2 vector2)
        {
            return new Vector2(Mathf.Abs(vector2.x), Mathf.Abs(vector2.y));
        }
    }
}