using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNavigatorManager : MonoBehaviour
{
    private Object[] MenuItems;

    public float MoveDelay;
    private float CurrentTime = 0;

    public List<Controller> AllControllers;

    private float[] ControllersStickX;
    private float[] ControllersStickY;

    private float[] ControllersDPadX;
    private float[] ControllersDPadY;

    // Start is called before the first frame update
    void Start()
    {
        MenuItems = GameObject.FindObjectsOfType<MenuNavigator>();
        ControllersStickX = new float[AllControllers.Count];
        ControllersStickY = new float[AllControllers.Count];
        ControllersDPadX = new float[AllControllers.Count];
        ControllersDPadY = new float[AllControllers.Count];
    }

    // Update is called once per frame
    void Update()
    {
        //CurrentTime = 0;

        for (int i = 0; i < AllControllers.Count; i++)
        {
            if (AllControllers[i].ControlState[Control.DPadXAxis] != 0 && ControllersDPadX[i] == 0)
            {
                if (AllControllers[i].ControlState[Control.DPadXAxis] > 0)
                {
                    //Moving to the right
                    foreach (MenuNavigator menuitem in MenuItems)
                    {
                        menuitem.GetInput(Direction.Right);
                    }
                }
                else if (AllControllers[i].ControlState[Control.DPadXAxis] < 0)
                {
                    //Moving to the left
                    foreach (MenuNavigator menuitem in MenuItems)
                    {
                        menuitem.GetInput(Direction.Left);
                    }
                }
            }

            if (AllControllers[i].ControlState[Control.LeftStickXAxis] != 0 && ControllersStickX[i] == 0)
            {
                if (AllControllers[i].ControlState[Control.LeftStickXAxis] > 0)
                {
                    //Moving to the right
                    foreach (MenuNavigator menuitem in MenuItems)
                    {
                        menuitem.GetInput(Direction.Right);
                    }
                }
                else if (AllControllers[i].ControlState[Control.LeftStickXAxis] < 0)
                {
                    //Moving to the left
                    foreach (MenuNavigator menuitem in MenuItems)
                    {
                        menuitem.GetInput(Direction.Left);
                    }
                }
            }

            if (AllControllers[i].ControlState[Control.DPadYAxis] != 0 && ControllersDPadY[i] == 0)
            {
                if (AllControllers[i].ControlState[Control.DPadYAxis] > 0)
                {
                    //Moving to the right
                    foreach (MenuNavigator menuitem in MenuItems)
                    {
                        menuitem.GetInput(Direction.Up);
                    }
                }
                else if (AllControllers[i].ControlState[Control.DPadYAxis] < 0)
                {
                    //Moving to the left
                    foreach (MenuNavigator menuitem in MenuItems)
                    {
                        menuitem.GetInput(Direction.Down);
                    }
                }
            }

            if (AllControllers[i].ControlState[Control.LeftStickYAxis] != 0 && ControllersStickY[i] == 0)
            {
                if (AllControllers[i].ControlState[Control.LeftStickYAxis] > 0)
                {
                    //Moving to the right
                    foreach (MenuNavigator menuitem in MenuItems)
                    {
                        menuitem.GetInput(Direction.Down);
                    }
                }
                else if (AllControllers[i].ControlState[Control.LeftStickYAxis] < 0)
                {
                    //Moving to the left
                    foreach (MenuNavigator menuitem in MenuItems)
                    {
                        menuitem.GetInput(Direction.Up);
                    }
                }
            }
        }

        
        foreach (Controller controller in AllControllers)
        { 
            if (controller.GetControlDown(Control.South))
            {
                //Selecting
                foreach (MenuNavigator menuitem in MenuItems)
                {
                    menuitem.GetInput(Direction.None);
                }
            }
        }


        for (int i = 0; i < AllControllers.Count; i++)
        {
            ControllersStickX[i] = AllControllers[i].ControlState[Control.LeftStickXAxis];
            ControllersStickY[i] = AllControllers[i].ControlState[Control.LeftStickYAxis];

            ControllersDPadX[i] = AllControllers[i].ControlState[Control.DPadXAxis];
            ControllersDPadY[i] = AllControllers[i].ControlState[Control.DPadYAxis];
        }

        CurrentTime += Time.deltaTime;
    }

    public void TestSelect(string button)
    {
        Debug.Log("Pressed Button " + button);
    }
}
