using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TitleText : MonoBehaviour
{
    [Header("Text objects")]
    public TextMeshProUGUI CyanText;
    public TextMeshProUGUI YellowText;
    public TextMeshProUGUI MagentaText;
    public TextMeshProUGUI KeyText;

    [Header("Positions")]
    public Vector2 StartPosition;
    public Vector2 MidPosition;
    public Vector2 EndPosition;

    [Header("Timings")]
    public float EntryTime;
    public float WaitTime;
    public float ExitTime;

    private RectTransform rect;

    public void Start()
    {
        rect = GetComponent<RectTransform>();

        YellowText.rectTransform.anchoredPosition = new Vector2(0, 0);
        MagentaText.rectTransform.anchoredPosition = new Vector2(-3, -1.5f);
        CyanText.rectTransform.anchoredPosition = new Vector2(-6, -3f);
        KeyText.rectTransform.anchoredPosition = new Vector2(-9, -4.5f);
    }

    public IEnumerator DisplayText(string text)
    {
        CyanText.text = text;
        YellowText.text = text;
        MagentaText.text = text;
        KeyText.text = text;

        rect.anchoredPosition = StartPosition;

        Vector2 EntrySpeed = (StartPosition - MidPosition) / EntryTime;
        while (rect.anchoredPosition.x > MidPosition.x)
        {
            if ((rect.anchoredPosition - (EntrySpeed * Time.deltaTime)).x < MidPosition.x)
            {
                rect.anchoredPosition = MidPosition;
                break;
            }

            rect.anchoredPosition = rect.anchoredPosition - (EntrySpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(WaitTime);

        Vector2 ExitSpeed = (MidPosition - EndPosition) / ExitTime;
        while (rect.anchoredPosition.x > EndPosition.x)
        {
            if ((rect.anchoredPosition - (EntrySpeed * Time.deltaTime)).x < EndPosition.x)
            {
                rect.anchoredPosition = EndPosition;
                break;
            }

            rect.anchoredPosition = rect.anchoredPosition - (ExitSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }

    public void StartText( string text)
    {
        StopAllCoroutines();
        StartCoroutine(DisplayText(text));
    }
}
