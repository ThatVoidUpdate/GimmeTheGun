using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainObject;
    public GameObject SettingsObject;

    public void PlayGame()
    {
        SceneManager.LoadScene("Complete2Player");
    }

    public void ActivateSettings()
    {
        MainObject.SetActive(false);
        SettingsObject.SetActive(true);

    }

    public void ActivateMain()
    {
        MainObject.SetActive(true);
        SettingsObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}