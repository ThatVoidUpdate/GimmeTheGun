using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TestingActionManager : MonoBehaviour
{

    public Light LeftLight;
    public Light RightLight;
    public void LightSwitchL()
    {
        RightLight.enabled = true;
    }

    public void LightSwitchR()
    {
        LeftLight.enabled = true;
    }
}
