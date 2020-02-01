using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNavigatorManager : MonoBehaviour
{
    private Object[] MenuItems;

    public float MoveDelay;
    private float CurrentTime = 0;

    public List<Controller> AllControllers;

    // Start is called before the first frame update
    void Start()
    {
        MenuItems = GameObject.FindObjectsOfType<MenuNavigator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentTime > MoveDelay)
        {
            //CurrentTime = 0;

            foreach (Controller controller in AllControllers)
            {
                if (controller.ControlState[Control.DPadXAxis] > 0 || controller.ControlState[Control.LeftStickXAxis] > 0 || controller.ControlState[Control.LeftStickXAxis] > 0)
                {
                    //Moving to the right
                    foreach (MenuNavigator menuitem in MenuItems)
                    {
                        menuitem.GetInput(Direction.Right);
                    }
                }
                else if (controller.ControlState[Control.DPadXAxis] < 0 || controller.ControlState[Control.LeftStickXAxis] < 0 || controller.ControlState[Control.LeftStickXAxis] < 0)
                {
                    //Moving to the left
                    foreach (MenuNavigator menuitem in MenuItems)
                    {
                        menuitem.GetInput(Direction.Left);
                    }
                }
                else if (controller.ControlState[Control.DPadYAxis] < 0 || controller.ControlState[Control.LeftStickYAxis] > 0 || controller.ControlState[Control.LeftStickYAxis] > 0)
                {
                    //Moving to the down
                    foreach (MenuNavigator menuitem in MenuItems)
                    {
                        menuitem.GetInput(Direction.Down);
                    }
                }
                else if (controller.ControlState[Control.DPadYAxis] > 0 || controller.ControlState[Control.LeftStickYAxis] < 0 || controller.ControlState[Control.LeftStickYAxis] < 0)
                {
                    //Moving to the up
                    foreach (MenuNavigator menuitem in MenuItems)
                    {
                        menuitem.GetInput(Direction.Up);
                    }
                }
                else if (controller.GetControlDown(Control.South))
                {
                    //Selecting
                    foreach (MenuNavigator menuitem in MenuItems)
                    {
                        menuitem.GetInput(Direction.None);
                    }
                }
            }
        }
        CurrentTime += Time.deltaTime;
    }

    public void TestSelect()
    {
        Debug.Log("Pressed Button");
    }
}
