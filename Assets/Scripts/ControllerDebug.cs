using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerDebug : MonoBehaviour
{
    public Controller controller;

    public Image LeftStickIndicator;
    public Image RightStickIndicator;
    public Image AIndicator;
    public Image BIndicator;
    public Image XIndicator;
    public Image YIndicator;
    public Image DPadIndicator;
    public Image MenuIndicator;
    public Image BackIndicator;

    public Image LeftBumperIndicator;
    public Image RightBumperIndicator;
    public Image LeftTriggerIndicator;
    public Image RightTriggerIndicator;

    private Vector3 LeftStickPosition;
    private Vector3 RightStickPosition;
    private Vector3 DpadPosition;
    private Vector3 LeftTriggerPosition;
    private Vector3 RightTriggerPosition;

    public float StickPositionMultiplier;

    public Color Normal;
    public Color Pressed;

    // Start is called before the first frame update
    void Start()
    {
        LeftStickPosition = LeftStickIndicator.transform.position;
        RightStickPosition = RightStickIndicator.transform.position;
        DpadPosition = DPadIndicator.transform.position;
        LeftTriggerPosition = LeftTriggerIndicator.transform.position;
        RightTriggerPosition = RightTriggerIndicator.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        AIndicator.color = controller.ControlState[Control.A] == 1 ? Pressed : Normal;
        BIndicator.color = controller.ControlState[Control.B] == 1 ? Pressed : Normal;
        XIndicator.color = controller.ControlState[Control.X] == 1 ? Pressed : Normal;
        YIndicator.color = controller.ControlState[Control.Y] == 1 ? Pressed : Normal;
        MenuIndicator.color = controller.ControlState[Control.Start] == 1 ? Pressed : Normal;
        BackIndicator.color = controller.ControlState[Control.Back] == 1 ? Pressed : Normal;
        LeftStickIndicator.color = controller.ControlState[Control.LeftStick] == 1 ? Pressed : Normal;
        RightStickIndicator.color = controller.ControlState[Control.RightStick] == 1 ? Pressed : Normal;
        LeftBumperIndicator.color = controller.ControlState[Control.LeftBumper] == 1 ? Pressed : Normal;
        RightBumperIndicator.color = controller.ControlState[Control.RightBumper] == 1 ? Pressed : Normal;

        LeftStickIndicator.transform.position = LeftStickPosition + new Vector3(controller.ControlState[Control.LeftStickXAxis] * StickPositionMultiplier, controller.ControlState[Control.LeftStickYAxis] * -StickPositionMultiplier, 0);
        RightStickIndicator.transform.position = RightStickPosition + new Vector3(controller.ControlState[Control.RightStickXAxis] * StickPositionMultiplier, controller.ControlState[Control.RightStickYAxis] * -StickPositionMultiplier, 0);
        DPadIndicator.transform.position = DpadPosition + new Vector3(controller.ControlState[Control.DPadXAxis] * StickPositionMultiplier, controller.ControlState[Control.DPadYAxis] * StickPositionMultiplier, 0);

        LeftTriggerIndicator.transform.position = LeftTriggerPosition + new Vector3(0, controller.ControlState[Control.LeftTrigger] * StickPositionMultiplier, 0);
        RightTriggerIndicator.transform.position = RightTriggerPosition + new Vector3(0, controller.ControlState[Control.RightTrigger] * StickPositionMultiplier, 0);



    }
}
