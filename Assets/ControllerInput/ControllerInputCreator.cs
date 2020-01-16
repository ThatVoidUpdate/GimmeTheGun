using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

public class ControllerInputCreator : EditorWindow
{
    int MaxControllers;
    float DeadZone;

    [MenuItem("Window/Controller Input")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        ControllerInputCreator window = (ControllerInputCreator)GetWindow(typeof(ControllerInputCreator));
        window.Show();
    }

    void OnGUI()
    {
        //Field to hold the maximum amount of controllers that can be accessed at the same time
        EditorGUILayout.LabelField("The max amount of controllers");
        MaxControllers = EditorGUILayout.IntField(MaxControllers);

        //The deadzone on controller axes, outside of which it will not return 0
        EditorGUILayout.LabelField("The deadzone in the middle of the axes");
        DeadZone = EditorGUILayout.FloatField(DeadZone);

        //Button to commence the rewriting
        if (GUILayout.Button("Write InputManager"))
        {
            //Holds the output file
            StringBuilder output = new StringBuilder("%YAML 1.1\n% TAG!u! tag: unity3d.com, 2011:\n---!u!13 & 1\nInputManager: \n  m_ObjectHideFlags: 0\n  serializedVersion: 2\n  m_Axes: ");
            for (int i = 0; i < MaxControllers; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    //for every controller, write a normal entry to the input manager
                    output.Append("\n  - serializedVersion: 3\n    m_Name: Joy");
                    output.Append(i.ToString());
                    output.Append("Axis");
                    output.Append(j.ToString());
                    output.Append("\n    descriptiveName:\n    descriptiveNegativeName:\n    negativeButton: \n    positiveButton:\n    altNegativeButton: \n    altPositiveButton:\n    gravity: 0");
                    output.Append("\n    dead: ");
                    output.Append(DeadZone.ToString());
                    output.Append("\n    sensitivity: 1\n    snap: 0\n    invert: 0\n    type: 2");
                    output.Append("\n    axis: ");
                    output.Append(j.ToString());
                    output.Append("\n    joyNum: ");
                    output.Append((i+1).ToString());
                }
                output.Append("\n");
            }

            //Write the new input managet to the inputmanager asset in unity
            File.WriteAllText(@"ProjectSettings\InputManager.asset", output.ToString());
            Debug.Log("Done rewriting input settings");
        }
    }
}
