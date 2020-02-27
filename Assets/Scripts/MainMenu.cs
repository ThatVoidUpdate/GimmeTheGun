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

    public void ChangeResolution(int index)
    {
        switch (index)
        {
            case 0:
                Screen.SetResolution(800, 600, Screen.fullScreen);
                break;
            case 1:
                Screen.SetResolution(1280, 720, Screen.fullScreen);
                break;
            case 2:
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
                break;
            default:
                break;
        }
    }

    public void SetFullscreen(bool Fullscreen)
    {
        Screen.fullScreen = Fullscreen;
    }
}