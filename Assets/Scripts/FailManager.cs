using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailManager : MonoBehaviour
{
    public GameObject FailOverlay;
    private bool HasFailed = false;
    private Controller[] controllers;
    public void Start()
    {
        controllers = FindObjectsOfType<Controller>();
    }
    
    public void Update()
    {
        if (HasFailed)
        {
            foreach (Controller controller in controllers)
            {
                if (controller.GetControlState(Control.South) == 1)
                {
                    //someone is pressing the A button, restart the level
                    SceneManager.LoadScene("Complete2Player");
                }
                if (controller.GetControlState(Control.East) == 1)
                {
                    //someone is pressing the B button, exit to the menu
                    SceneManager.LoadScene("Menu");
                }
            }
        }
    }

    public void OnFail()
    {
        if (!HasFailed)
        {
            FailOverlay.SetActive(true);
            foreach (Enemy enemy in GameObject.FindObjectsOfType<Enemy>())
            {
                enemy.Speed = 0;
                enemy.Damage = 0;
            }
            foreach (Player player in GameObject.FindObjectsOfType<Player>())
            {
                player.HorizontalSpeed = 0;
                player.VerticalSpeed = 0;
                player.SetCanMove(false);
                player.HeldObject = null;
            }

            HasFailed = true;
        }
    }
}
