using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//public enum Control
//{
//    LeftStickXAxis, LeftStickYAxis, Triggers, RightStickXAxis, RightStickYAxis, DPadXAxis, DPadYAxis, Unused, LeftTrigger, RightTrigger,
//    A, B, X, Y, LeftBumper, RightBumper, Back, Start, LeftStick, RightStick
//}

public class FakeController : MonoBehaviour
{
    [Header("Controller ID")]
    [Tooltip("The controller index to listen to, as given by Unity")]
    public int ControllerID = 0;

    [Header("Controller State")]
    public bool[] Buttons = new bool[10];
    public float[] Axes = new float[10];

    public Dictionary<Control, float> ControlState = new Dictionary<Control, float>();

    public void Start()
    {
        //We need to check that the controller is in a valid range. We cant listen to a controller below 0.
        if (ControllerID < 0)
        {
            Debug.LogError("ControllerID is too small, setting to 0");
            ControllerID = 0;
        }

        //We initialise the output dictionary, with each key being every item in the Control enum (Initialized to 0).
        foreach (Control control in Enum.GetValues(typeof(Control)))
        {
            ControlState.Add(control, 0);
        }
    }

    void Update()
    {
        //Loop over every button on the specific controller, and save its value to the control dictionary
        for (int i = 0; i < 10; i++)
        {
            //Set the correct button to the button state, in both the array and the dictionary
            Buttons[i] = Input.GetKey("joystick " + (ControllerID + 1) + " button " + i);
            ControlState[(Control)(i + 10)] = Buttons[i] ? 1 : 0;
        }

        for (int i = 0; i < 10; i++)
        {
            //Set the correct axis to the axis value in the array and the dictionary
            Axes[i] = Input.GetAxis("Joy" + ControllerID + "Axis" + i);
            ControlState[(Control)i] = Axes[i];
        }
    }
}
