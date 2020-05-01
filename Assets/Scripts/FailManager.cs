using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class FailManager : MonoBehaviour
{
    public GameObject FailOverlay;
    private bool HasFailed = false;
    
    public void Update()
    {
        if (HasFailed)
        {
            if (Input.GetAxis("Submit") == 1)
            {
                //someone is pressing the A button, restart the level
                SceneManager.LoadScene("Complete2Player");
            }
            if (Input.GetAxis("Cancel") == 1)
            {
                //someone is pressing the B button, exit to the menu
                SceneManager.LoadScene("MenuCharlie");
            }
            
        }
    }

    public void OnFail()
    {
        if (!HasFailed)
        {
            FailOverlay.SetActive(true);
            foreach (Enemy enemy in FindObjectsOfType<Enemy>())
            {
                enemy.Speed = 0;
                enemy.Damage = 0;
            }
            foreach (Player player in FindObjectsOfType<Player>())
            {
                player.RespawnTime = 999999999999999999; //31 billion years should be enough
            }

            HasFailed = true;
        }
    }
}
