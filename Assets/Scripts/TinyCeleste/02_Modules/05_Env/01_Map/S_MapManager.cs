using System.Collections.Generic;
using TinyCeleste._01_Framework;

namespace TinyCeleste._02_Modules._05_Env._01_Map
{
    public class S_MapManager : EntitySingleton<S_MapManager>
    {
        public List<E_Map> mapList;
    }
}