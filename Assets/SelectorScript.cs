using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorScript : MonoBehaviour {

    public GameObject Football;
    public GameObject Scientist;
    public GameObject Soldier;

    private Vector3 CharacterPosition;
    private Vector3 OffScreen;

    private int CharacterInt = 1;

    private SpriteRenderer ScientistRender, FootballRender, SoldierRender;

    //stating the sprites
    private void Awake()
    {
        CharacterPosition = Soldier.transform.position;
        OffScreen = Scientist.transform.position;
        SoldierRender = Soldier.GetComponent<SpriteRenderer>();
        ScientistRender = Scientist.GetComponent<SpriteRenderer>();
        FootballRender = Football.GetComponent<SpriteRenderer>();

    }

    //This is used to render in the next character and move the previous character off screen
    public void NextCharacter()
    {
        switch (CharacterInt)
        {
            case 1:
                SoldierRender.enabled = false;
                Soldier.transform.position = OffScreen;
                Scientist.transform.position = CharacterPosition;
                ScientistRender.enabled = true;
                CharacterInt++;
                break;
            case 2:
                ScientistRender.enabled = false;
                Scientist.transform.position = OffScreen;
                Football.transform.position = CharacterPosition;
                FootballRender.enabled = true;
                CharacterInt++;
                break;
            case 3:
                FootballRender.enabled = false;
                Football.transform.position = OffScreen;
                Soldier.transform.position = CharacterPosition;
                SoldierRender.enabled = true;
                CharacterInt++;
                ResetInt();
                break;
            default:
                ResetInt();
                break;
        }
    }

    
    //made for the restart so once its cycled through all the characters it resets.
    private void ResetInt()
    {
        if(CharacterInt >=3)
        {
            CharacterInt = 1;
        }
        else
        {
            CharacterInt = 4;
        }
    }
    
    
}
