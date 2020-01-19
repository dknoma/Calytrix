using UnityEditor;
using UnityEngine;

namespace Utility {
    public class AssetHelper {
        /// <summary>
        /// Saves a given asset to the given path with the given filename
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="path"></param>
        /// <param name="filename"></param>
        public static void SaveAssetToDatabase(Object asset, string path, string filename) {
            AssetDatabase.CreateAsset(asset, GetAssetPath(path, filename));
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();
        }
        
        public static string GetAssetPath(string path, string sourcename) {
            return $"{path}/{sourcename}.asset";
        }
    }
}