using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds all the buttons and axes on a standard xbox controller. In this order, they can be accessed in the normal order given by unity, and buttons are just +10 to the normal index
/// </summary>
public enum Control
{
    LeftStickXAxis, LeftStickYAxis, Triggers, RightStickXAxis, RightStickYAxis, DPadXAxis, DPadYAxis, Unused, LeftTrigger, RightTrigger,
    A, B, X, Y, LeftBumper, RightBumper, Back, Start, LeftStick, RightStick
}

/// <summary>
/// Listens to a specific controller, and exposes all of its buttons and axes
/// </summary>
public class Controller : MonoBehaviour
{
    [Header("Controller ID")]
    [Tooltip("The controller index to listen to, as given by Unity")]
    public int ControllerID = 0;

    [Header("Controller State")]
    public bool[] Buttons = new bool[10];
    public float[] Axes = new float[10];

    public Dictionary<Control, float> ControlState = new Dictionary<Control, float>();

    //Used for GetControllerDown and GetControllerUp.
    private int ControlStateInt;
    private int OldControlStateInt;

    [Space]
    [Tooltip("Enabling this will give a debug log of every button pressed and axis moved, every frame")]
    public bool Verbose;

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
    public void Update()
    {
        OldControlStateInt = ControlStateInt;
        ControlStateInt = 0;

        string output = "";

        //Loop over every button on the specific controller, and save its value to the control dictionary
        for (int i = 0; i < 10; i++)
        {
            if (Verbose)
            {
                //If we are printing the result, check if the button is actually pressed, and append to the output string if needed
                if (Input.GetKey("joystick " + (ControllerID + 1) + " button " + i))
                {
                    output += "Joystick " + ControllerID + " button " + i + " Held\n";
                }
            }
            //Set the correct button to the button state, in both the array and the dictionary
            Buttons[i] = Input.GetKey("joystick " + (ControllerID + 1) + " button " + i);
            ControlState[(Control)(i + 10)] = Buttons[i] ? 1 : 0;
            ControlStateInt += Buttons[i] ? (int)Mathf.Pow(2, i + 10): 0;
        }

        for (int i = 0; i < 10; i++)
        {
            if (Verbose)
            {
                //If we are printing the result, check if the axis isnt 0, and append to the output string if needed
                if (Input.GetAxis("Joy" + ControllerID + "Axis" + i) != 0)
                {
                    output += "Joystick " + ControllerID + " Axis " + i + ": " + Input.GetAxis("Joy" + ControllerID + "Axis" + i) + "\n";
                }
            }
            //Set the correct axis to the axis value in the array and the dictionary
            Axes[i] = Input.GetAxis("Joy" + ControllerID + "Axis" + i);
            ControlState[(Control)i] = Axes[i];
            ControlStateInt += Axes[i] == 1 ? (int)Mathf.Pow(2, i) : 0;
        }

        if (output != "" && Verbose)
        {
            Debug.Log(output);
        }
    }

    /// <summary>
    /// Returns true on the first frame that the control is pressed
    /// </summary>
    /// <param name="control">The control to check for</param>
    /// <returns>True on the first frame that the control is pressed, otherwise false</returns>
    public bool GetControllerDown(Control control)
    {
        int ControlsDown = ~OldControlStateInt & ControlStateInt;
        return (ControlsDown & (1 << (int)control)) != 0;
    }

    /// <summary>
    /// Returns true on the first frame that the control is released
    /// </summary>
    /// <param name="control">The control to check for</param>
    /// <returns>True on the first frame that the control is released, otherwise false</returns>
    public bool GetControllerUp(Control control)
    {
        int ControlsDown = OldControlStateInt & ~ControlStateInt;
        return (ControlsDown & (1 << (int)control)) != 0;
    }
}
