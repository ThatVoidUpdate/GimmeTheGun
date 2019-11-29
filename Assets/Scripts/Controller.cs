using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Control
{
    LeftStickXAxis, LeftStickYAxis, Triggers, RightStickXAxis, RightStickYAxis, DPadXAxis, DPadYAxis, Unused, LeftTrigger, RightTrigger,
    A, B, X, Y, LeftBumper, RightBumper, Back, Start, LeftStick, RightStick
}

public class Controller : MonoBehaviour
{
    [Header("Controller ID")]
    public int ControllerID = 0;

    [Header("Controller State")]
    public bool[] Buttons = new bool[10];
    public float[] Axes = new float[10];

    public Dictionary<Control, float> ControlState = new Dictionary<Control, float>();

    [Space]
    public bool Verbose;



    public void Start()
    {
        if (ControllerID > 1)
        {
            Debug.LogError("ControllerID is too large, setting to 4");
            ControllerID = 1;
        }
        if (ControllerID < 0)
        {
            Debug.LogError("ControllerID is too small, setting to 0");
            ControllerID = 0;
        }

        foreach (Control control in Enum.GetValues(typeof(Control)))
        {
            ControlState.Add(control, 0);
        }
    }
    void Update()
    {
        string output = "";
        for (int i = 0; i < 10; i++)
        {
            if (Input.GetKey("joystick " + (ControllerID + 1) + " button " + i))
            {
                output += "Joystick " + ControllerID + " button " + i + " Held\n";
                
            }
            Buttons[i] = Input.GetKey("joystick " + (ControllerID + 1) + " button " + i);
            ControlState[(Control)(i + 10)] = Buttons[i] ? 1 : 0;
        }

        for (int i = 0; i < 10; i++)
        {
            if (Input.GetAxis("Joy" + ControllerID + "Axis" + i) != 0)
            {
                output += "Joystick " + ControllerID + " Axis " + i + ": " + Input.GetAxis("Joy" + ControllerID + "Axis" + i) + "\n";
            }
            Axes[i] = Input.GetAxis("Joy" + ControllerID + "Axis" + i);
            ControlState[(Control)i] = Axes[i];
        }

        if (output != "" && Verbose)
        {
            Debug.Log(output);
        }
        
    }
}
