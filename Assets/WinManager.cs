using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    public GameObject WinOverlay;
    private bool HasWon = false;

    public void Update()
    {
        if (HasWon)
        {
            if (Input.GetAxis("Submit") == 1)
            {
                //someone is pressing the A button, restart the level
                SceneManager.LoadScene("Complete2Player");
            }
            if (Input.GetAxis("Cancel") == 1)
            {
                //someone is pressing the B button, exit to the menu
                SceneManager.LoadScene("Menu");
            }

        }
    }

    public void OnWin()
    {
        if (!HasWon)
        {
            WinOverlay.SetActive(true);
            foreach (Enemy enemy in FindObjectsOfType<Enemy>())
            {
                enemy.Speed = 0;
                enemy.Damage = 0;
            }
            foreach (Player player in FindObjectsOfType<Player>())
            {
                player.SetCanMove(false);
                player.HorizontalLookControl = null;
                player.VerticalLookControl = null;
                // player.RespawnTime = 999999999999999999; //31 billion years should be enough
            }

            HasWon = true;
        }
    }
}
