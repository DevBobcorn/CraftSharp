using System.IO;
using UnityEngine;

namespace CraftSharp
{
    public class PathHelper
    {
        private static readonly char SP = Path.DirectorySeparatorChar;

        public static string GetRootDirectory()
        {
            return Application.persistentDataPath;
        }

        public static string GetPacksDirectory()
        {
            return Application.persistentDataPath + $"{SP}Resource Packs";
        }

        public static string GetPackDirectoryNamed(string packName)
        {
            return Application.persistentDataPath + $"{SP}Resource Packs{SP}{packName}";
        }

        public static string GetPackFile(string packName, string fileName)
        {
            return Application.persistentDataPath + $"{SP}Resource Packs{SP}{packName}{SP}{fileName}";
        }

        public static string GetExtraDataDirectory()
        {
            return Application.persistentDataPath + $"{SP}Extra Data";
        }

        public static string GetExtraDataFile(string fileName)
        {
            return Application.persistentDataPath + $"{SP}Extra Data{SP}{fileName}";
        }
    }
}