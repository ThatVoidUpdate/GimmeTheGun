using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Controller controller;
    public bool Released;

    public bool MenuOpen = false;

    public GameObject MenuHolder;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.ControlState[Control.Start] == 1 && Released)
        {
            MenuOpen = !MenuOpen;
            if (MenuOpen)
            {
                MenuHolder.SetActive(true);
            }
            else
            {
                MenuHolder.SetActive(false);
            }
            Released = false;
            
        }

        if (controller.ControlState[Control.Start] == 0 && !Released)
        {
            Released = true;
        }

        if (controller.ControlState[Control.A] == 1 && MenuOpen)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
