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
        
        
    }

    IEnumerator TosplashTwo()
    {
        yield return new WaitForSeconds(5);
        SceneNumber = 1;
        SceneManager.LoadScene(1);
    }

    
}
