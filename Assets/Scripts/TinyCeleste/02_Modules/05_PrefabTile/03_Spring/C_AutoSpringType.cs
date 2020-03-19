using System.Collections.Generic;
using TinyCeleste._01_Framework;
using UnityEngine;

namespace TinyCeleste._02_Modules._05_PrefabTile._03_Spring
{
    public class C_AutoSpringType : EntityComponent
    {
        public enum Enum_SpringType
        {
            Out,
            In
        }

        public List<Sprite> spriteList;

        public Enum_SpringType springType;

        public SpriteRenderer spriteRenderer;
        
        private void OnValidate()
        {
            spriteRenderer.sprite = spriteList[(int) springType];
        }
    }
}