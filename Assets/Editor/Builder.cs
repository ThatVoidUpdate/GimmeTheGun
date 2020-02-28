using UnityEditor;
using System.Diagnostics;

public class Builder
{
    [MenuItem("Build/Windows Build including lines.txt")]
    public static void BuildGame()
    {
        // Get filename.
        string path = EditorUtility.SaveFolderPanel("Choose Location of Built Game", "", "");
        string[] levels = new[] { "Assets/Scenes/Menu.unity", "Assets/Scenes/Complete2Player.unity" };

        // Build player.
        BuildPipeline.BuildPlayer(levels, path + "/BuiltGame.exe", BuildTarget.StandaloneWindows, BuildOptions.None);

        // Copy a file from the project folder to the build folder, alongside the built game.
        FileUtil.CopyFileOrDirectory("Assets/lines.txt", path + "/lines.txt");
    }
}
