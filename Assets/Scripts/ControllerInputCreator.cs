using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

public class ControllerInputCreator : EditorWindow
{
    int Controllers;

    [MenuItem("Window/Controllers")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        ControllerInputCreator window = (ControllerInputCreator)EditorWindow.GetWindow(typeof(ControllerInputCreator));
        window.Show();
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("The max amount of controllers");
        Controllers = EditorGUILayout.IntField(Controllers);

        if (GUILayout.Button("Write InputManager"))
        {
            StringBuilder output = new StringBuilder("%YAML 1.1\n% TAG!u! tag: unity3d.com, 2011:\n---!u!13 & 1\nInputManager: \n  m_ObjectHideFlags: 0\n  serializedVersion: 2\n  m_Axes: ");
            for (int i = 0; i < Controllers; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    output.Append("\n  - serializedVersion: 3\n    m_Name: Joy");
                    output.Append(i.ToString());
                    output.Append("Axis");
                    output.Append(j.ToString());
                    output.Append("\n    descriptiveName:\n    descriptiveNegativeName:\n    negativeButton: \n    positiveButton:\n    altNegativeButton: \n    altPositiveButton:\n    gravity: 0\n    dead: 0.19\n    sensitivity: 1\n    snap: 0\n    invert: 0\n    type: 2");
                    output.Append("\n    axis: ");
                    output.Append(j.ToString());
                    output.Append("\n    joyNum: ");
                    output.Append((i+1).ToString());
                }
                output.Append("\n");
            }

            
            File.WriteAllText(@"ProjectSettings\InputManager.asset", output.ToString());
            Debug.Log("Done rewriting input settings");

        }
    }
}
