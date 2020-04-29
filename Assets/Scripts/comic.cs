using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class comic : MonoBehaviour
{
    public Sprite[] panels;
    public float[] timings;

    public float FadeTime;

    public Image displayImage;
    private int index = 0;

    void Start()
    {
        if (panels.Length != timings.Length)
        {
            Debug.LogError("There needs to be a time for every comic (comic.cs)");
        }
        displayImage.sprite = panels[index];
        StartCoroutine(ComicWait());
    }


    void Update()
    {
        if (Input.GetAxis("Submit") == 1)
        {
            //skip cutscene
            StopAllCoroutines();
            SceneManager.LoadScene("Complete2Player");
        }
    }

    public IEnumerator ComicWait()
    {
        yield return new WaitForSeconds(timings[index]);
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut()
    {
        print("Fading out");
        float currentFade = FadeTime;

        while (currentFade >= 0)
        {
            currentFade -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
            displayImage.color = new Color(currentFade / FadeTime, currentFade / FadeTime, currentFade / FadeTime);
        }

        index++;
        if (index == timings.Length)
        {
            //reached end of comic
            SceneManager.LoadScene("Complete2Player");
        }
        else
        {
            displayImage.sprite = panels[index];
            StartCoroutine(FadeIn());
        }

    }

    public IEnumerator FadeIn()
    {
        print("Fading in");
        float currentFade = 0;

        while (currentFade <= FadeTime)
        {
            currentFade += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            displayImage.color = new Color(currentFade / FadeTime, currentFade / FadeTime, currentFade / FadeTime);
        }

        StartCoroutine(ComicWait());
    }
}
