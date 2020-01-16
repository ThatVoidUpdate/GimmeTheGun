using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//public enum Control
//{
//    LeftStickXAxis, LeftStickYAxis, Triggers, RightStickXAxis, RightStickYAxis, DPadXAxis, DPadYAxis, Unused, LeftTrigger, RightTrigger,
//    A, B, X, Y, LeftBumper, RightBumper, Back, Start, LeftStick, RightStick
//}

public class FakeController : Controller
{
    //[Header("Controller ID")]
    //[Tooltip("The controller index to listen to, as given by Unity")]
    //public int ControllerID = 0;

    //[Header("Controller State")]
    //public bool[] Buttons = new bool[10];
    //public float[] Axes = new float[10];

    [Header("Keyboard - Controller assignments")]
    public KeyCode LeftStickXAxisPositive;
    public KeyCode LeftStickXAxisNegative;
    public KeyCode LeftStickYAxisPositive;
    public KeyCode LeftStickYAxisNegative;
    public KeyCode RightStickXAxisPositive;
    public KeyCode RightStickXAxisNegative;
    public KeyCode RightStickYAxisPositive;
    public KeyCode RightStickYAxisNegative;

    public KeyCode DPadUp;
    public KeyCode DPadDown;
    public KeyCode DPadLeft;
    public KeyCode DPadRight;

    public KeyCode LeftTrigger;
    public KeyCode RightTrigger;

    public KeyCode A;
    public KeyCode B;
    public KeyCode X;
    public KeyCode Y;

    public KeyCode LeftBumper;
    public KeyCode RightBumper;

    public KeyCode BackButton;
    public KeyCode StartButton;
    public KeyCode LeftStick;
    public KeyCode RightStick;


    //public Dictionary<Control, float> ControlState = new Dictionary<Control, float>();

    public new void Start()
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

    public new void Update()
    {
        ControlState[Control.LeftStickXAxis] = Input.GetKey(LeftStickXAxisPositive) ? 1 : (Input.GetKey(LeftStickXAxisNegative) ? -1 : 0);
        ControlState[Control.LeftStickYAxis] = Input.GetKey(LeftStickYAxisPositive) ? 1 : (Input.GetKey(LeftStickYAxisNegative) ? -1 : 0);
        ControlState[Control.RightStickXAxis] = Input.GetKey(RightStickXAxisPositive) ? 1 : (Input.GetKey(RightStickXAxisNegative) ? -1 : 0);
        ControlState[Control.RightStickYAxis] = Input.GetKey(RightStickYAxisPositive) ? 1 : (Input.GetKey(RightStickYAxisNegative) ? -1 : 0);

        ControlState[Control.DPadXAxis] = Input.GetKey(DPadLeft) ? 1 : (Input.GetKey(DPadRight) ? -1 : 0);
        ControlState[Control.DPadYAxis] = Input.GetKey(DPadUp) ? 1 : (Input.GetKey(DPadDown) ? -1 : 0);

        ControlState[Control.LeftTrigger] = Input.GetKey(LeftTrigger) ? 1 : 0;
        ControlState[Control.RightTrigger] = Input.GetKey(RightTrigger) ? 1 : 0;
        ControlState[Control.Triggers] = Input.GetKey(RightTrigger) ? 1 : (Input.GetKey(LeftTrigger) ? -1 : 0);

        ControlState[Control.South] = Input.GetKey(A) ? 1 : 0;
        ControlState[Control.East] = Input.GetKey(B) ? 1 : 0;
        ControlState[Control.West] = Input.GetKey(X) ? 1 : 0;
        ControlState[Control.North] = Input.GetKey(Y) ? 1 : 0;

        ControlState[Control.LeftBumper] = Input.GetKey(LeftBumper) ? 1 : 0;
        ControlState[Control.RightBumper] = Input.GetKey(RightBumper) ? 1 : 0;

        ControlState[Control.Back] = Input.GetKey(BackButton) ? 1 : 0;
        ControlState[Control.Start] = Input.GetKey(StartButton) ? 1 : 0;
        ControlState[Control.LeftStick] = Input.GetKey(LeftStick) ? 1 : 0;
        ControlState[Control.RightStick] = Input.GetKey(RightStick) ? 1 : 0;

        for (int i = 0; i < 10; i++)
        {
            Buttons[i] = ControlState[(Control)(i + 10)] == 1;
        }
        for (int i = 0; i < 10; i++)
        {
            Axes[i] = ControlState[(Control)i];
        }
    }
}
