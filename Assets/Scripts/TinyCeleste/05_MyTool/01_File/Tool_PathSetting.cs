using System.IO;
using UnityEngine;

namespace TinyCeleste._05_MyTool._01_File
{
    public static class Tool_PathSetting
    {
        /// <summary>
        /// Unity项目根目录
        /// 示例: C:/Users/wyj/Desktop/Unity Project 1/Assets
        /// </summary>
        public static string AssetPath => Application.dataPath;

        /// <summary>
        /// 根目录下的Resources文件夹路径
        /// 若不存在，会自动进行创建
        /// 示例: C:/Users/wyj/Desktop/Unity Project 1/Assets/Resources
        /// </summary>
        public static string ResourcesPath
        {
            get
            {
                string path = Application.dataPath + "/Resources";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return path;
            }
        }

        /// <summary>
        /// 本地存储路径
        /// 示例: C:/Users/wyj/AppData/LocalLow/DefaultCompany/Unity Project 1
        /// </summary>
        public static string LocalPath => Application.persistentDataPath;
    }
}