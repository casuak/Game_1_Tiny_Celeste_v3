

using TinyCeleste._02_Modules._05_Env._01_Map._03_Utils;
#if UNITY_EDITOR
using TinyCeleste._02_Modules._05_Env._01_Map._01_MapEditor;
using UnityEngine;

namespace TinyCeleste._02_Modules._05_Env._01_Map
{
    public partial class E_Map
    {
        [HideInInspector]
        public MapEditor mapEditor;
        
        [ContextMenu("Clear")]
        public void Clear()
        {
            mapEditor.ClearMap();
        }
        
        [HideInInspector]
        public MapElementArray2 elementDic;

        [HideInInspector]
        public MapElementArray2 hideDic;
    }
}

#endif