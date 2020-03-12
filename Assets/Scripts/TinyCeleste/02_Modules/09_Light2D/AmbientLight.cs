using UnityEngine;

namespace TinyCeleste._02_Modules._09_Light2D
{
    public class AmbientLight : MonoBehaviour
    {
        public SpriteRenderer lightSprite;
        
        private void Awake()
        {
            lightSprite.enabled = true;
        }
    }
}