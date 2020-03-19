using System;
using TinyCeleste._01_Framework;

namespace TinyCeleste._02_Modules._04_Effect._02_Snow
{
    public class E_Snow : Entity
    {
        public C_NineSquare nineSquare;

        private void Update()
        {
            nineSquare.S_UpdateNineSnowBlock();
        }

        private void OnDrawGizmosSelected()
        {
            nineSquare.S_DrawNineSquare();
        }
    }
}