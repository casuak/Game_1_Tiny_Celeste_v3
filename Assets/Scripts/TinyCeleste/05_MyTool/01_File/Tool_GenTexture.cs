using System.IO;
using UnityEngine;

namespace TinyCeleste._05_MyTool._01_File
{
    public class Tool_GenTexture
    {
        /// <summary>
        /// 存储生成贴图的相对路径（相对于Assets目录）
        /// </summary>
        public static readonly string relativePath = "Resources/Generated Textures";
        
        /// <summary>
        /// 存储生成贴图的绝对路径
        /// </summary>
        public static readonly string absolutePath = Application.dataPath + "/" + relativePath;

        /// <summary>
        /// 保存Texture到"Resources/Generated Textures"文件夹
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        public static void SaveTexture2DToPNG(Texture2D texture, string path, string fileName)
        {
            byte[] bytes = texture.EncodeToPNG();
            string dirPath = absolutePath + "/" + path;
            string fullPath = dirPath + "/" + fileName;
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            FileStream file = File.Open(fullPath, FileMode.Create);
            BinaryWriter writer = new BinaryWriter(file);
            writer.Write(bytes);
            file.Close();
        }
    }
}