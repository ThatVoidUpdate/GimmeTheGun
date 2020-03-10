using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugMenu : MonoBehaviour
{
    private TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            text.enabled = true;
        }

        if (text.enabled)
        {
            text.text = "";
            foreach (Enemy enemy in FindObjectsOfType<Enemy>())
            {
                text.text += enemy.transform.position + "\n";
            }
        }
    }
}
