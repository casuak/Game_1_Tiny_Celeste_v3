using UnityEngine;

namespace TinyCeleste._04_Extension._02_Unity
{
    public static class EX_LayerMask
    {
        public static bool ContainLayer(this LayerMask layerMask, int layer)
        {
            return layerMask.ContainSameLayer(1 << layer);
        }

        public static bool ContainSameLayer(this LayerMask layerMask, LayerMask another)
        {
            return (layerMask & another) != 0;
        }
    }
}