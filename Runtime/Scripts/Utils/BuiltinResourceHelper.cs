#nullable enable
using System;
using System.Collections;
using System.IO;
using System.IO.Compression;
using UnityEngine;

namespace CraftSharp
{
    public static class BuiltinResourceHelper
    {
        private static readonly char SP = Path.DirectorySeparatorChar;

        /// <summary>
        /// Id of the file containing currently present resource version.
        /// </summary>
        public static string RES_VERSION_FILE_NAME = "RES_VERSION";

        /// <summary>
        /// Generate or update builtin resource files.
        /// </summary>
        public static IEnumerator ReadyBuiltinResource(string resName, int resVersion, string targetFolder, Action<string> updateStatus, Action start, Action<bool> complete)
        {
            if (!Directory.Exists(targetFolder)) // Not present yet, try generating it
            {
                return ExtractBuiltinResource(resName, resVersion, targetFolder,
                        overwriteFiles: false, updateStatus, start, complete);
            }
            else
            {
                bool shouldUpdatePresentRes = false;

                if (File.Exists($"{targetFolder}{SP}{RES_VERSION_FILE_NAME}"))
                {
                    var presentResVersion = File.ReadAllText($"{targetFolder}{SP}{RES_VERSION_FILE_NAME}");
                    int.TryParse(presentResVersion, out int presentResVersionNum);

                    if (presentResVersionNum < resVersion) // This version present in data folder is outdated, update it
                    {
                        shouldUpdatePresentRes = true;
                        Debug.Log($"Resource [{resName}] version {presentResVersionNum} is outdated. Updating to version {resVersion}...");
                    }
                    else
                    {
                        Debug.Log($"Resource [{resName}] version {presentResVersionNum} is up-to-date.");
                    }
                }
                else
                {
                    shouldUpdatePresentRes = true;
                    Debug.Log($"Resource [{resName}] doesn't have a version tag. Updating to version {resVersion}...");
                }

                if (shouldUpdatePresentRes) // Also generate the files (overwriting old files with same names)
                {
                    return ExtractBuiltinResource(resName, resVersion, targetFolder,
                            overwriteFiles: true, updateStatus, start, complete);
                }
                else
                {
                    return CallbacksOnly(start, complete);
                }
            }
        }

        private static IEnumerator CallbacksOnly(Action start, Action<bool> complete)
        {
            start.Invoke();

            yield return null;

            complete.Invoke(true);
        }

        private static IEnumerator ExtractBuiltinResource(string resName, int resVersion, string targetFolder, bool overwriteFiles, Action<string> updateStatus, Action start, Action<bool> complete)
        {
            Debug.Log($"Generating builtin resource from [{resName}]");

            start.Invoke();

            yield return null;

            bool succeeded = false;

            var resAsset = Resources.Load(resName) as TextAsset;

            if (resAsset != null && resAsset.bytes != null) // Data bytes loaded, unzip it
            {
                try
                {
                    var zipStream = new MemoryStream(resAsset.bytes);
                    using (var zipFile = new ZipArchive(zipStream, ZipArchiveMode.Read))
                    {
                        updateStatus("resource.info.extract_builtin_resource");
                        // Extract resouce files
                        // TODO: Support Unicode characters in entry names. Currently it can only properly handle ASCII.
                        zipFile.ExtractToDirectory(targetFolder, overwriteFiles);

                        Debug.Log($"Data asset [{resName}] successfully generated.");
                    }

                    // And add the version tag
                    File.WriteAllText($"{targetFolder}{SP}{RES_VERSION_FILE_NAME}", resVersion.ToString());

                    succeeded = true;
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"Exception occurred when extracting resource asset [{resName}]: {e}");
                }
            }
            else
            {
                Debug.LogWarning($"Resource asset [{resName}] cannot be retrieved!");
            }

            yield return null;

            complete.Invoke(succeeded);
        }
    }
}
