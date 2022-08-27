 /*
 This script generates a list of scenes in the build whenever
 a new build is made. It is needed because unity does not provide
 a way to get a list of scenes or their names at runtime.
 */

using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using System.IO;

public class SceneLister : IPreprocessBuild {

    public int callbackOrder { get { return 0; } }

    public void OnPreprocessBuild(BuildTarget target, string path) {
        SaveScenesList();
    }


    [MenuItem("Custom/Save ScenesList")]
    private static void SaveScenesList() {
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;

        // Check if list of scenes exists
        ScenesList list = (ScenesList)AssetDatabase.LoadAssetAtPath("Assets/Resources/ScenesList.asset", typeof(ScenesList));

        // Create it if it does not exist
        if (list == null) {
            list = ScriptableObject.CreateInstance<ScenesList>();
            AssetDatabase.CreateAsset(list, "Assets/Resources/ScenesList.asset");
        }

        // Fill the array with the level names
        list.scenesNames = new string[scenes.Length];
        for (int i = 0; i < scenes.Length; ++i) {
            list.scenesNames[i] = Path.GetFileNameWithoutExtension(scenes[i].path);
        }

        // Writes the changes
        AssetDatabase.SaveAssets();
    }
}
