using UnityEngine;

namespace TinyCeleste._05_MyTool._02_Game
{
    public class Tool_Layer
    {
        public static int AddLayer(int originalLayer, string layerName)
        {
            return AddLayer(originalLayer, LayerMask.NameToLayer(layerName));
        }
        
        public static int AddLayer(int originalLayer, int layIndex)
        {
            originalLayer |= 1 << layIndex;
            return originalLayer;
        }

        public static int RemoveLayer(int originalLayer, string layerName)
        {
            return RemoveLayer(originalLayer, LayerMask.NameToLayer(layerName));
        }
        
        public static int RemoveLayer(int originalLayer, int layerIndex)
        {
            originalLayer &= ~(1 << layerIndex);
            return originalLayer;
        }
    }
}