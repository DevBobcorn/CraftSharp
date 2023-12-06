using System.IO;
using UnityEngine;

namespace CraftSharp
{
    public class PathHelper
    {
        private static readonly char SP = Path.DirectorySeparatorChar;

        public static string GetRootDirectory()
        {
            return Directory.GetParent(Application.persistentDataPath).FullName;
        }

        public static string GetPacksDirectory()
        {
            return Directory.GetParent(Application.persistentDataPath).FullName + $"{SP}Resource Packs";
        }

        public static string GetPackDirectoryNamed(string packName)
        {
            return Directory.GetParent(Application.persistentDataPath).FullName + $"{SP}Resource Packs{SP}{packName}";
        }

        public static string GetPackFile(string packName, string fileName)
        {
            return Directory.GetParent(Application.persistentDataPath).FullName + $"{SP}Resource Packs{SP}{packName}{SP}{fileName}";
        }

        public static string GetExtraDataDirectory()
        {
            return Directory.GetParent(Application.persistentDataPath).FullName + $"{SP}Extra Data";
        }

        public static string GetExtraDataFile(string fileName)
        {
            return Directory.GetParent(Application.persistentDataPath).FullName + $"{SP}Extra Data{SP}{fileName}";
        }
    }
}