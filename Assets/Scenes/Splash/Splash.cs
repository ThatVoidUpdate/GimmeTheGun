using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{

    public static int SceneNumber;

    void Start()
    {

        if (SceneNumber == 0)
        {
            StartCoroutine(TosplashTwo ());
        }
        if (SceneNumber == 1)
        {
            StartCoroutine(ToMenuCharlie ());
        }
    }

    IEnumerator TosplashTwo()
    {
        yield return new WaitForSeconds(5);
        SceneNumber = 2;
        SceneManager.LoadScene(2);
    }

    IEnumerator ToMenuCharlie ()
    {
        yield return new WaitForSeconds(5);
        SceneNumber = 2;
        SceneManager.LoadScene(2);
    }

}
