using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerTest : MonoBehaviour
{
    public Control InteractControl;
    public Controller controller;

    private Image self;

    // Start is called before the first frame update
    void Start()
    {
        self = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

        float value = controller.ControlState[InteractControl];
        self.color = new Color(value, value, value);
    }

    public void SetInput(int control)
    {
        InteractControl = (Control)control;
    }
}
