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
        AIndicator.color = controller.GetControlState(Control.South) == 1 ? Pressed : Normal;
        BIndicator.color = controller.GetControlState(Control.East) == 1 ? Pressed : Normal;
        XIndicator.color = controller.GetControlState(Control.West) == 1 ? Pressed : Normal;
        YIndicator.color = controller.GetControlState(Control.North) == 1 ? Pressed : Normal;
        MenuIndicator.color = controller.GetControlState(Control.Start) == 1 ? Pressed : Normal;
        BackIndicator.color = controller.GetControlState(Control.Back) == 1 ? Pressed : Normal;
        LeftStickIndicator.color = controller.GetControlState(Control.LeftStick) == 1 ? Pressed : Normal;
        RightStickIndicator.color = controller.GetControlState(Control.RightStick) == 1 ? Pressed : Normal;
        LeftBumperIndicator.color = controller.GetControlState(Control.LeftBumper) == 1 ? Pressed : Normal;
        RightBumperIndicator.color = controller.GetControlState(Control.RightBumper) == 1 ? Pressed : Normal;

        LeftStickIndicator.transform.position = LeftStickPosition + new Vector3(controller.GetControlState(Control.LeftStickXAxis) * StickPositionMultiplier, controller.GetControlState(Control.LeftStickYAxis) * -StickPositionMultiplier, 0);
        RightStickIndicator.transform.position = RightStickPosition + new Vector3(controller.GetControlState(Control.RightStickXAxis) * StickPositionMultiplier, controller.GetControlState(Control.RightStickYAxis) * -StickPositionMultiplier, 0);
        DPadIndicator.transform.position = DpadPosition + new Vector3(controller.GetControlState(Control.DPadXAxis) * StickPositionMultiplier, controller.GetControlState(Control.DPadYAxis) * StickPositionMultiplier, 0);

        LeftTriggerIndicator.transform.position = LeftTriggerPosition + new Vector3(0, controller.GetControlState(Control.LeftTrigger) * StickPositionMultiplier, 0);
        RightTriggerIndicator.transform.position = RightTriggerPosition + new Vector3(0, controller.GetControlState(Control.RightTrigger) * StickPositionMultiplier, 0);



    }
}
