using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorScript2 : MonoBehaviour { 

    public GameObject Soldier1;
    public GameObject Football1;
    public GameObject Scientist1;
    private Vector3 CharacterPosition1;
    private Vector3 Offscreen1;
    private int CharacterInt1 = 1;
    private SpriteRenderer Soldier1Render, Football1Render, Scientist1Render;

    private void Awake()
    {
        CharacterPosition1 = Soldier1.transform.position;
        Offscreen1 = Football1.transform.position;
        Soldier1Render = Soldier1.GetComponent<SpriteRenderer>();
        Football1Render = Soldier1.GetComponent<SpriteRenderer>();
        Scientist1Render = Soldier1.GetComponent<SpriteRenderer>();
    }

    public void NextCharacter1()
    {
        switch(CharacterInt1)
        {
            case 1:
                Soldier1Render.enabled = false;
                Soldier1.transform.position = Offscreen1;
                Football1.transform.position = CharacterPosition1;
                Football1Render.enabled = true;
                CharacterInt1++;
                break;
            case 2:
                Football1Render.enabled = false;
                Football1.transform.position = Offscreen1;
                Scientist1.transform.position = CharacterPosition1;
                Scientist1Render.enabled = true;
                CharacterInt1++;
                break;
            case 3:
                Scientist1Render.enabled = false;
                Scientist1.transform.position = Offscreen1;
                Soldier1.transform.position = CharacterPosition1;
                Soldier1Render.enabled = true;
                CharacterInt1++;
                ResetInt1();
                break;
            default:
                ResetInt1();
                break;


        }

    }
    private void ResetInt1()
    {
        if (CharacterInt1 >= 3)
        {
            CharacterInt1 = 1;
        }
        else
        {
            CharacterInt1 = 4;
        }

    }


}
