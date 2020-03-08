using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds all the buttons and axes on a standard xbox controller. In this order, they can be accessed in the normal order given by unity, and buttons are just +10 to the normal index
/// </summary>
public enum Control
{
    LeftStickXAxis, LeftStickYAxis, Triggers, RightStickXAxis, RightStickYAxis, DPadXAxis, DPadYAxis, Unused, LeftTrigger, RightTrigger,
    South, East, West, North, LeftBumper, RightBumper, Back, Start, LeftStick, RightStick
}

/// <summary>
/// Listens to a specific controller, and exposes all of its buttons and axes
/// </summary>
public class Controller : MonoBehaviour
{
    [Header("Controller ID")]
    [Tooltip("The controller index to listen to, as given by Unity")]
    [SerializeField]
    [Range(0, 20)]
    protected int ControllerID = 0;

    [Header("Controller State")]
    [SerializeField]
    protected bool[] Buttons = new bool[10];
    [SerializeField]
    protected float[] Axes = new float[10];


    protected Dictionary<Control, float> ControlState = new Dictionary<Control, float>();

    //Used for GetControlDown and GetControlUp.
    protected int ControlStateInt;
    protected int OldControlStateInt;

    [Space]
    [Tooltip("Enabling this will give a debug log of every button pressed and axis moved, every frame")]
    public bool Verbose;

    public void Start()
    {
        //We initialise the output dictionary, with each key being every item in the Control enum (Initialized to 0).
        foreach (Control control in Enum.GetValues(typeof(Control)))
        {
            ControlState.Add(control, 0);
        }
    }
    public void Update()
    {
        //Reset the control states, so that GetControllerUp and GetControllerDown work
        OldControlStateInt = ControlStateInt;
        ControlStateInt = 0;

        //Debug output
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
        {//Log all the output if needed
            Debug.Log(output);
        }
    }

    /// <summary>
    /// Returns true on the first frame that the control is pressed
    /// </summary>
    /// <param name="control">The control to check for</param>
    /// <returns>True on the first frame that the control is pressed, otherwise false</returns>
    public bool GetControlDown(Control control)
    {
        //ControlsDown becomes 1 wherever the old state was up, and the new state is down. 0 everywhere else
        int ControlsDown = ~OldControlStateInt & ControlStateInt;

        //Extract the correct bit of the changed controls, and check if it is not 0
        return (ControlsDown & (1 << (int)control)) != 0;
    }

    /// <summary>
    /// Returns true on the first frame that the control is released
    /// </summary>
    /// <param name="control">The control to check for</param>
    /// <returns>True on the first frame that the control is released, otherwise false</returns>
    public bool GetControlUp(Control control)
    {
        //ControlsDown becomes 1 wherever the old state was down, and the new state is up. 0 everywhere else
        int ControlsUp = OldControlStateInt & ~ControlStateInt;

        //Extract the correct bit of the changed controls, and check if it is not 0
        return (ControlsUp & (1 << (int)control)) != 0;
    }

    /// <summary>
    /// Returns the private controller id
    /// </summary>
    /// <returns>The id of the controller</returns>
    public int GetControllerID()
    {
        return ControllerID;
    }

    /// <summary>
    /// Sets the controller id, between 0 and 20
    /// </summary>
    /// <param name="_ControllerID">The number to set the id to</param>
    public void SetControllerID(int _ControllerID)
    {
        if (_ControllerID >= 0 && _ControllerID <= 20)
        {
            ControllerID = _ControllerID;
        }
    }

    /// <summary>
    /// Returns the state of the selected control
    /// </summary>
    /// <param name="_Control">The control to measure</param>
    /// <returns>The value of the control</returns>
    public float GetControlState(Control _Control)
    {
        return ControlState[_Control];
    }
}
