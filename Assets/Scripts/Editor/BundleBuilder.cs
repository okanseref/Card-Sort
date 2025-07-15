using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class BundleBuilder
    {
        [MenuItem("Tools/Build AssetBundles")]
        public static void BuildAllAssetBundles()
        {
            string bundleDirectory = "Assets/StreamingAssets";

            if (!Directory.Exists(bundleDirectory))
            {
                Directory.CreateDirectory(bundleDirectory);
            }

            // Optional: Choose target platform
            BuildTarget target = EditorUserBuildSettings.activeBuildTarget;

            BuildPipeline.BuildAssetBundles(bundleDirectory, BuildAssetBundleOptions.None, target);

            Debug.Log("AssetBundles built to: " + bundleDirectory);
        }
    }
}