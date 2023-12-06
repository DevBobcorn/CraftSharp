using System.IO;
using UnityEngine;

namespace CraftSharp
{
    public class PathHelper
    {
        private static readonly char SP = Path.DirectorySeparatorChar;
        private static string appPersistentData = string.Empty;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void OnBeforeSplashScreen()
        {
            // Cache this into a variable because Application.persistentDataPath is not thread-safe
            // Accessing it from resource loader thread etc will lead to exceptions
            appPersistentData = Application.persistentDataPath;
        }

        public static string GetRootDirectory()
        {
            return appPersistentData;
        }

        public static string GetPacksDirectory()
        {
            return appPersistentData + $"{SP}Resource Packs";
        }

        public static string GetPackDirectoryNamed(string packName)
        {
            return appPersistentData + $"{SP}Resource Packs{SP}{packName}";
        }

        public static string GetPackFile(string packName, string fileName)
        {
            return appPersistentData + $"{SP}Resource Packs{SP}{packName}{SP}{fileName}";
        }

        public static string GetExtraDataDirectory()
        {
            return appPersistentData + $"{SP}Extra Data";
        }

        public static string GetExtraDataFile(string fileName)
        {
            return appPersistentData + $"{SP}Extra Data{SP}{fileName}";
        }
    }
}