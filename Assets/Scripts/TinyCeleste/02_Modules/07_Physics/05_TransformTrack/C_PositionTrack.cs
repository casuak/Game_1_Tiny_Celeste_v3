using TinyCeleste._01_Framework;
using UnityEngine;

namespace TinyCeleste._02_Modules._07_Physics._05_TransformTrack
{
    public class C_PositionTrack : EntityComponent
    {
        public Transform target;

        private void Update()
        {
            transform.position = target.position;
        }
    }
}