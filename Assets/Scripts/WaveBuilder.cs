using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using System.IO;

public class WaveBuilder : EditorWindow
{
    private static List<GameObject> SpawnObjects = new List<GameObject>();
    private static List<int> SpawnAmounts = new List<int>();

    public string FileName = "Filename...";

    private int Length = 1;

    [MenuItem("Window/Wave Builder")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(WaveBuilder));
        SpawnObjects.Add(null);
        SpawnAmounts.Add(0);
    }

    void OnGUI()
    {
        GUILayout.Label("Enemies", EditorStyles.boldLabel);

        for (int i = 0; i < Length; i++)
        {
            SpawnObjects[i] = (GameObject)EditorGUILayout.ObjectField("Enemy to spawn", SpawnObjects[i], typeof(GameObject), false);
            SpawnAmounts[i] = EditorGUILayout.IntField("Amount to spawn", SpawnAmounts[i]);
            EditorGUILayout.Space();
        }

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Add"))
        {
            Length++;
            SpawnObjects.Add(null);
            SpawnAmounts.Add(0);
        }

        if (GUILayout.Button("Remove Last"))
        {
            if (Length > 1)
            {
                Length--;
                SpawnObjects.RemoveAt(SpawnObjects.Count - 1);
                SpawnAmounts.RemoveAt(SpawnAmounts.Count - 1);
            }            
        }

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        FileName = EditorGUILayout.TextField(FileName);

        if (GUILayout.Button("Done"))
        {
            //Write out the file
            StringBuilder output = new StringBuilder("using System.Collections.Generic;\nusing UnityEngine;\npublic class " + FileName + " : MonoBehaviour\n{\npublic List<(GameObject, int)> Enemies;\npublic void Start()\n{ Enemies = new List<(GameObject, int)> { ");

            for (int i = 0; i < SpawnObjects.Count; i++)
            {
                output.Append("(Resources.Load<GameObject>(\"Enemies/");
                output.Append(SpawnObjects[i].name);
                output.Append("\"), ");
                output.Append(SpawnAmounts[i]);
                output.Append("), ");
            }
            output.Remove(output.Length - 2, 2);

            output.Append(" };\nGetComponent<Spawner>().Wave = Enemies;\n}\n}");

            File.WriteAllText(@"Assets\Waves\" + FileName + ".cs", output.ToString());
            Debug.Log("File Written");
        }

        GUILayout.EndHorizontal();
    }
}
