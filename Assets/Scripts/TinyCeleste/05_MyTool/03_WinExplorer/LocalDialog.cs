using System;
using System.Runtime.InteropServices;
using TinyCeleste._05_MyTool._01_File;

namespace TinyCeleste._05_MyTool._03_WinExplorer
{
    public static class LocalDialog
    {
        // 链接指定系统函数       打开文件对话框
        [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern bool GetOpenFileName([In, Out] OpenFileName ofn);

        // 链接指定系统函数        另存为对话框
        [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern bool GetSaveFileName([In, Out] OpenFileName ofn);

        [DllImport("shell32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SHBrowseForFolder([In, Out] OpenDirName ofn);

        [DllImport("shell32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern bool SHGetPathFromIDList([In] IntPtr pidl, [In, Out] char[] fileName);

        // 打开资源管理器，获取文件地址（输入或选择）
        // 返回空表明直接关闭了窗口
        public static string GetFilePath(string fileName = "New File", string tip = "Asset文件(*.asset)",
            string suffix = ".asset")
        {
            var ofn = new OpenFileName(fileName);
            ofn.filter = tip + "\0*" + suffix;
            bool success = GetSaveFileName(ofn);
            // 直接关闭窗口则返回null
            if (!success) return null;
            int len = Tool_PathSetting.AssetPath.Length;
            // 去除Assets之前的路径
            string path = "Assets" + ofn.file.Substring(len);
            // 检查是否以suffix结尾
            if (!path.EndsWith(suffix))
            {
                path += suffix;
            }

            return path;
        }

        // 打开资源管理器，获取选择的文件夹地址（不选择则返回空字符串）
        public static string GetDirPath2()
        {
            OpenDirName odn = new OpenDirName();
            // 存放目录路径缓冲区
            odn.pszDisplayName = new string(new char[2000]);
            // 标题
            odn.lpszTitle = "Open Project";
            // 新的样式,带编辑框
//            odn.ulFlags = BIF_NEWDIALOGSTYLE | BIF_EDITBOX;
            IntPtr ptr = SHBrowseForFolder(odn);
            char[] chars = new char[2000];
            for (int i = 0; i < chars.Length; i++)
                chars[i] = '\0';
            SHGetPathFromIDList(ptr, chars);
            string fullPath = new string(chars);
            string path = fullPath.Substring(0, fullPath.IndexOf('\0'));
            return path;
        }
    }
}