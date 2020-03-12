#if UNITY_EDITOR
using UnityEditor;

namespace TinyCeleste._02_Modules._05_Env._01_Map._01_MapEditor
{
    public partial class MapEditor
    {
        // 进入编辑模式
        private void OnEnterEditMode()
        {
            Tools.current = Tool.None;
//            ActiveEditorTracker.sharedTracker.isLocked = true;
//            ActiveEditorTracker.sharedTracker.ForceRebuild();
        }

        // 退出编辑模式
        public void OnExitEditMode()
        {
//            ActiveEditorTracker.sharedTracker.isLocked = false;
//            ActiveEditorTracker.sharedTracker.ForceRebuild();
            currentBrush?.OnExit();
            currentBrush = null;
            Repaint();
        }

        // 当前选择的颜料发生改变
        public void OnSelectedColorChange()
        {
            currentBrush?.OnSelectedColorChange();
        }
    }
}

#endif