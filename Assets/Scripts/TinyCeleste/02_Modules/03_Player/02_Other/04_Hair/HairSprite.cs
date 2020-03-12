using TinyCeleste._01_Framework;
using UnityEngine;

namespace TinyCeleste._02_Modules._03_Player._02_Other._04_Hair
{
    public class HairSprite : EntityComponent
    {
        public SpriteRenderer sr;

        public void SetColor(Color color)
        {
            sr.color = color;
        }
    }
}