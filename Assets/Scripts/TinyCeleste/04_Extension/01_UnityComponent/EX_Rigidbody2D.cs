using UnityEngine;

namespace TinyCeleste._04_Extension._01_UnityComponent
{
    public static class EX_Rigidbody2D
    {
        public static void SetXSpeed(this Rigidbody2D rigidbody2D, float xSpeed)
        {
            Vector2 newVelocity = rigidbody2D.velocity;
            newVelocity.x = xSpeed;
            rigidbody2D.velocity = newVelocity;
        }
        
        public static void SetYSpeed(this Rigidbody2D rigidbody2D, float ySpeed)
        {
            Vector2 newVelocity = rigidbody2D.velocity;
            newVelocity.y = ySpeed;
            rigidbody2D.velocity = newVelocity;
        }
    }
}